using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LX.StaffScheduler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeController : ControllerBase
    {
        private readonly IDistrictService _svc;

        public CafeController(IDistrictService service)
        {
            _svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DistrictDTO>>> Get()
        {
            var result = await _svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictDTO>> GetById(int id)
        {
            var district = await _svc.GetByIdAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            return Ok(district);
        }

        [HttpPost]
        public async Task<ActionResult<DistrictDTO>> Post(DistrictDTO districtDTO)
        {
            if (districtDTO == null)
            {
                return BadRequest("District data is null");
            }
            try
            {
                var createdDistrict = await _svc.AddAsync(districtDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdDistrict.Id }, createdDistrict);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

