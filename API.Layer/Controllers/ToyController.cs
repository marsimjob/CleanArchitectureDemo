using Application.Layer.Commands.CreateToy;
using Application.Layer.Commands.DeleteToy;
using Application.Layer.Queries.GetAllQuery;
using Application.Layer.Queries.GetToyByID;
using Domain.Layer.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Layer
{
    [ApiController]
    [Route("api/toys")]
    public class ToyController : ControllerBase
    {
        private readonly IMediator _sender;

        public ToyController(IMediator sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToyDto>>> GetAll()
        {

            var result = await _sender.Send(new GetAllToysQuery());
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ToyDto>> GetToyById(int id)
        {
            var result = await _sender.Send(new GetToyByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ToyDto>> PostToy([FromBody] CreateToyCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> SoftDeleteToy(int id)
        {
            {
                await _sender.Send(new DeleteToyCommand(id));
                return NoContent();
            }
        }
    }
}
