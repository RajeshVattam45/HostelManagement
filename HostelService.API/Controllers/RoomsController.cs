using HostelService.Application.DTOs;
using HostelService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelService.API.Controllers
{
    [ApiController]
    [Route ( "api/[controller]" )]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomAppService _service;

        public RoomsController ( IRoomAppService service )
        {
            _service = service;
        }

        [HttpGet ( "{id}" )]
        public async Task<IActionResult> Get ( int id )
        {
            var room = await _service.GetByIdAsync ( id );
            return room == null ? NotFound () : Ok ( room );
        }

        [HttpGet ( "hostel/{hostelId}" )]
        public async Task<IActionResult> GetByHostel ( int hostelId )
        {
            var rooms = await _service.GetAllByHostelAsync ( hostelId );
            return Ok ( rooms );
        }

        [HttpPost]
        public async Task<IActionResult> Create ( [FromBody] CreateRoomDto dto )
        {
            var result = await _service.AddAsync ( dto );
            return CreatedAtAction ( nameof ( Get ), new { id = result.Id }, result );
        }

        [HttpPut ( "{id}" )]
        public async Task<IActionResult> Update ( int id, [FromBody] UpdateRoomDto dto )
        {
            await _service.UpdateAsync ( id, dto );
            return NoContent ();
        }

        [HttpDelete ( "{id}" )]
        public async Task<IActionResult> Delete ( int id )
        {
            await _service.DeleteAsync ( id );
            return NoContent ();
        }
    }
}
