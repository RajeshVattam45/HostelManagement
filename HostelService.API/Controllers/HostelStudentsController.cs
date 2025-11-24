using HostelService.Application.DTOs;
using HostelService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelService.API.Controllers
{
    [ApiController]
    [Route ( "api/[controller]" )]
    public class HostelStudentsController : ControllerBase
    {
        private readonly IHostelStudentAppService _service;

        public HostelStudentsController ( IHostelStudentAppService service )
        {
            _service = service;
        }

        [HttpGet ( "{id}" )]
        public async Task<IActionResult> Get ( int id )
        {
            var hs = await _service.GetByIdAsync ( id );
            return hs == null ? NotFound () : Ok ( hs );
        }

        [HttpGet ( "hostel/{hostelId}" )]
        public async Task<IActionResult> GetByHostel ( int hostelId )
        {
            var list = await _service.GetByHostelAsync ( hostelId );
            return Ok ( list );
        }

        [HttpPost]
        public async Task<IActionResult> Create ( [FromBody] CreateHostelStudentDto dto )
        {
            var result = await _service.AddAsync ( dto );
            return CreatedAtAction ( nameof ( Get ), new { id = result.Id }, result );
        }

        [HttpPut ( "{id}" )]
        public async Task<IActionResult> Update ( int id, [FromBody] UpdateHostelStudentDto dto )
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
