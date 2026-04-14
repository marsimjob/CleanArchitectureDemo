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
    }
}
