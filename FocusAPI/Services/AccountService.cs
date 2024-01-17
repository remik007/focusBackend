using FocusAPI.Data;
using FocusAPI.Exceptions;
using FocusAPI.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FocusAPI.Services
{
    public interface IAccountService
    {
        AccountToken RegisterUser(RegisterUserDto dto);
        LoginOutputDto GenerateToken(LoginDto dto);
        LoginOutputDto RefreshToken(TokensDto dto);
        AccountToken GetResetPasswordToken(string login);
        void ResetPassword(ResetPasswordDto resetPasswordDto);
        void ConfirmAccount(ConfirmAccountDto confirmAccountDto);
    }
    public class AccountService : IAccountService
    {
        private readonly FocusDbContext _context;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly AppSettings _appSettings;
        public AccountService(FocusDbContext context, IPasswordHasher<AppUser> passwordHasher, AuthenticationSettings authenticationSettings, AppSettings appSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _appSettings = appSettings;
        }
        public AccountToken RegisterUser(RegisterUserDto dto)
        {
            var newUser = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                UserRoleId = dto.UserRoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.Password = hashedPassword;
            _context.AppUsers.Add(newUser);
            _context.SaveChanges();
            var accountToken = GenerateAccountToken(newUser.Id);
            _context.Entry(accountToken).Reference(x => x.User).Load();
            return accountToken;
        }

        public LoginOutputDto GenerateToken(LoginDto dto)
        {
            var user = _context.AppUsers
                .Include(u => u.UserRole)
                .FirstOrDefault(u => u.Email == dto.Login || u.UserName == dto.Login);
            if (user == null)
            {
                throw new BadRequestException("Invalid login or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid login or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.UserRole.Name}"),
                new Claim(ClaimTypes.Email, $"{user.Email}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtValidityInMinutes);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_authenticationSettings.RefreshTokenValidityInDays);
            _context.AppUsers.Update(user);
            _context.SaveChanges();

            return new LoginOutputDto()
            {
                Email = dto.Login,
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                IsAdmin = user.UserRole.Name == "Admin"
            };
        }

        

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public LoginOutputDto RefreshToken(TokensDto dto)
        {
            if (dto is null || dto.AccessToken is null || dto.RefreshToken is null)
            {
                throw new BadRequestException("Invalid request data.");
            }

            string accessToken = dto.AccessToken;
            string refreshToken = dto.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                throw new BadRequestException("Invalid access token or refresh token");
            }

            var user = _context.AppUsers.Include(x => x.UserRole).FirstOrDefault(x => x.Email == principal.Identity.Name);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new BadRequestException("Invalid access token or refresh token");
            }

            var newRefreshToken = GenerateRefreshToken();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.UserRole.Name}"),
                new Claim(ClaimTypes.Email, $"{user.Email}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtValidityInMinutes);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_authenticationSettings.RefreshTokenValidityInDays);
            _context.AppUsers.Update(user);
            _context.SaveChanges();

            return new LoginOutputDto()
            {
                Email = user.Email,
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = newRefreshToken,
                Expiration = token.ValidTo,
                IsAdmin = user.UserRole.Name == "Admin"
            };
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        public AccountToken GetResetPasswordToken(string login)
        {
            var userId = _context.AppUsers.FirstOrDefault(u => u.Email == login || u.UserName == login).Id;
            var accountToken = GenerateAccountToken(userId);
            return accountToken;
        }

        public void ConfirmAccount(ConfirmAccountDto confirmAccountDto)
        {
            var tokenExist = _context.AccountTokens
                            .Include(x => x.User)
                            .Any(u => u.Token == confirmAccountDto.Token && u.Created.AddDays(_appSettings.AccountTokenExpireDays) > DateTime.Now && (u.User.Email == confirmAccountDto.Login || u.User.UserName == confirmAccountDto.Login));
            if (tokenExist)
            {
                _context.AppUsers.Where(u => u.Email == confirmAccountDto.Login || u.UserName == confirmAccountDto.Login).ExecuteUpdate(x => x.SetProperty(u => u.IsConfirmed, true));
            }
        }

        public void ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var tokenExist = _context.AccountTokens
                .Include(x => x.User)
                .Any(u => u.Token == resetPasswordDto.Token && u.Created.AddDays(_appSettings.AccountTokenExpireDays) > DateTime.Now && (u.User.Email == resetPasswordDto.Login || u.User.UserName == resetPasswordDto.Login));
            if (tokenExist)
            {
                var user = _context.AppUsers.FirstOrDefault(u => u.Email == resetPasswordDto.Login || u.UserName == resetPasswordDto.Login);
                var hashedPassword = _passwordHasher.HashPassword(user, resetPasswordDto.Password);
                user.Password = hashedPassword;
                _context.AppUsers.Where(u => u.Id == user.Id).ExecuteUpdate(x => x.SetProperty(u => u.Password, hashedPassword));
                DeleteAccountToken(resetPasswordDto.Token);
                //EF4.0
                //_context.AppUsers.Attach(user);
                //_context.Entry(user).Property(x => x.Password).IsModified = true;
                //_context.SaveChanges();
            }
        }

        private AccountToken GenerateAccountToken(int userId)
        {
            var token = GenerateRandomString();
            var accountToken = new AccountToken()
            {
                UserId = userId,
                Token = token,
                Created = DateTime.Now
            };
            _context.AccountTokens.Add(accountToken);
            _context.SaveChanges();
            return accountToken;
        }

        private void DeleteAccountToken(string token)
        {
            _context.AccountTokens.Where(t => t.Token == token).ExecuteDelete();
        }

        private String GenerateRandomString()
        {
            Random rand = new Random();

            // Choosing the size of string 
            // Using Next() string 
            int stringlen = _appSettings.AccountTokenLength;
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {

                // Generating a random number. 
                randValue = rand.Next(0, 26);

                // Generating random character by converting 
                // the random number into character. 
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string. 
                str = str + letter;
            }
            return str;
        }

    }
}
