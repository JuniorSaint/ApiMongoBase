using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Email;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route(template: "api/v1/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private ISendEmailSerivce _service;
        public SendEmailController(ISendEmailSerivce service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task EnviaEmail([FromBody] SendEmailDto email)
        {
            try
            {
                await _service.SendMail(email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
