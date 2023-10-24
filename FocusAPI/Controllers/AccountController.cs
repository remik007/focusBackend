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
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            _accountService.RegisterUser(registerUserDto);
            //SEND EMAIL
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
            //SEND EMAIL
            return Ok();
        }
    }
}
