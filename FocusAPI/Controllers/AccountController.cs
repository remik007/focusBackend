using FocusAPI.Models;
using FocusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocusAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        public AccountController(IAccountService accountService, IEmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var accountToken = _accountService.RegisterUser(registerUserDto);
            var emailRequest = new EmailRequest
            {
                To = accountToken.User.Email,
                Body = $"TOKEN do potwierdzenia konta: {accountToken.Token}",
                Subject = "Biuro Podróży FOCUS. Potwierdź swoje konto."
            };
            bool result = _emailService.Send(emailRequest, new CancellationToken());
            return Ok();
        }

        [HttpPost("login")]
        public string Login([FromBody] LoginDto loginDto)
        {
            string token = _accountService.GenerateToken(loginDto);
            return token;
        }

        [HttpPost("confirm")]
        public ActionResult ConfirmAccount([FromBody] ConfirmAccountDto confirmAccountDto)
        {
            _accountService.ConfirmAccount(confirmAccountDto);
            return Ok();
        }

        [HttpPost("resetpassword")]
        public ActionResult ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            _accountService.ResetPassword(resetPasswordDto);
            return Ok();
        }

        [HttpPost("getresetpasswordtoken")]
        public ActionResult GetResetPasswordToken([FromBody] GetResetPasswordDto getResetPasswordDto)
        {
            var accountToken = _accountService.GetResetPasswordToken(getResetPasswordDto.Login);
            var emailRequest = new EmailRequest
            {
                To = accountToken.User.Email,
                Body = $"TOKEN do zresetowania hasła: {accountToken.Token}",
                Subject = "Biuro Podróży FOCUS. Zresetuj swoje hasło."
            };
            bool result = _emailService.Send(emailRequest, new CancellationToken());
            return Ok();
        }
    }
}
