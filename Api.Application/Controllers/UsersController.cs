using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services;
using Api.Domain.Dtos.User;
using Microsoft.AspNetCore.Cors;

namespace Api.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private IUserService _service { get; set; }
        public UsersController(IUserService service)
        {
            _service = service;
        }

        // [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{skip}/{take}")]
        public async Task<ActionResult> GetAllPage([FromRoute] int skip, [FromRoute] int take)
        {
            try
            {
                return Ok(await _service.GetAllPage(skip, take));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // [Authorize("Bearer")]
        [HttpGet]
        [Route(template: "{id:guid}", Name = "GetWithId")]
        public async Task<ActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.Get(id);
                if (result == null)
                {
                    return NotFound($"Pesquisa não obteve êxito com Id: {id}");
                }
                return Ok(await _service.Get(id));

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //  [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserCreateDto user)
        {
            try
            {
                var result = await _service.Post(user);
                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetById", new { id = result.Id })), result);
                }

                return BadRequest();
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //   [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserUpdateDto user)
        {
            try
            {
                var result = await _service.Put(user);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Dados não foram atualizados");
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //   [Authorize("Bearer")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.Get(id);
                if (result == null)
                {
                    return NotFound($"Deleção não obteve êxito com Id: {id}");
                }
                return Ok(await _service.Delete(id));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPatch("password")]
        public async Task<ActionResult> PatchPassword([FromBody] UserPasswordUpdateDto user)
        {
            try
            {
                var result = await _service.PatchPassword(user);
                if (result != false)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Dados não foram atualizados");
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}

