using HostelService.Application.DTOs;
using HostelService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelService.API.Controllers
{
[ApiController]
    [Route("api/[controller]")]
    public class HostelsController : ControllerBase
    {
        private readonly IHostelAppService _service;

        public HostelsController(IHostelAppService service)
        {
            _service = service;
        }

        // GET: api/hostels
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/hostels/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var hostel = await _service.GetByIdAsync(id);
            if (hostel == null)
                return NotFound(new { message = "Hostel not found" });

            return Ok(hostel);
        }

        // POST: api/hostels
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHostelDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/hostels/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateHostelDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "ID mismatch" });

            try
            {
                await _service.UpdateAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/hostels/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
