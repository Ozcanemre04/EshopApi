using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Auth;
using Eshop.Application.Dtos.Response.Auth;
using Eshop.Application.Interfaces.Service;
using Eshop.Application.Validations.Auth;
using Eshop.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterDtoResponse>> Register(RegisterDtoRequest registerDtoRequest)
        {
            try
            {
                var validator = new RegisterDtoValidation();
                var ValidationResult = await validator.ValidateAsync(registerDtoRequest);
                if (!ValidationResult.IsValid)
                {
                    return BadRequest(ValidationResult.Errors);
                }
                var response = await _authService.Register(registerDtoRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginDtoResponse>> Login(LoginDtoRequest loginDtoRequest)
        {
            try
            {
                var validator = new LoginDtoValidation();
                var ValidationResult = await validator.ValidateAsync(loginDtoRequest);
                if (!ValidationResult.IsValid)
                {
                    return BadRequest(ValidationResult.Errors);
                }
                var response = await _authService.Login(loginDtoRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("exist"))
                {
                    return NotFound(ex.Message);
                }
                else if (ex.Message.Contains("wrong password"))
                {
                    return Unauthorized(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Refresh-Token")]
        public async Task<ActionResult<LoginDtoResponse>> RefreshToken(RefreshTokenDtoRequest refreshTokenDtoRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = await _authService.RefreshToken(refreshTokenDtoRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("exist")){
                    return NotFound(ex.Message);
                }
                else if(ex.Message =="Invalid or expired refresh token"){
                    return Unauthorized(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("Make-admin")]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<ActionResult<string>> MakeAdmin([FromBody] string email)
        {
            try
            {
                var response = await _authService.MakeAdmin(email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("found"))
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
    }
}