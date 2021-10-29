using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Login;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route(template: "api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private ILoginService _service;
        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _service.FindByLoginAsync(loginDto);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound($"Senha ou email {loginDto.Email} não encontrado");
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}