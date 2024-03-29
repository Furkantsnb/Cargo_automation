﻿using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public IActionResult Register(UserAndUnitRegisterDto userAndUnitRegisterDto)
        {
            var userExists = _authService.UserExists(userAndUnitRegisterDto.UserForRegister.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
           
            var registerResult = _authService.Register(userAndUnitRegisterDto.UserForRegister, userAndUnitRegisterDto.UserForRegister.Password,userAndUnitRegisterDto.UnitId);
            var result = _authService.CreateAccessToken(registerResult.Data,registerResult.Data.UnitId);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }

        [HttpPost("registerForRegister")]
        public IActionResult RegisterSecondAccount(UserForRegisterToSecondAccountDto userForRegister)
        {
            var userExists = _authService.UserExists(userForRegister.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            var registerResult = _authService.RegisterSecondAccount(userForRegister, userForRegister.Password,userForRegister.UnitId);
            var result = _authService.CreateAccessToken(registerResult.Data, userForRegister.UnitId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            var userToLogin = _authService.Login(userForLogin);
            if(!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            if(userToLogin.Data.IsActive)
            {
                var userUnit = _authService.GetUnit(userToLogin.Data.Id).Data;
                var result = _authService.CreateAccessToken(userToLogin.Data,userUnit.UnitId);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Kullanıcı Pasif Durumda. Aktif Etmke İçin Yöneticinize Danışın");

           
        }

        [HttpGet("confirmUser")]
        public IActionResult ConfirmUser(string value)
        {
            var user = _authService.GetByMailConfirmValue(value).Data;
            user.MailConfirm = true;
            user.MailConfirmDate=DateTime.Now;
           var result= _authService.Update(user);
            if(result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
           
        }


        [HttpGet("sendConfirmEmail")]
        public IActionResult SendConfirmEmail(int id)
        {
            var user = _authService.GetById(id).Data;
            var result = _authService.SendConfirmEmail(user);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);


        }
    }
}
