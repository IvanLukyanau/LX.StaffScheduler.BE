using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LX.StaffScheduler.BLL;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.Api.DependencyInjection;
using LX.StaffScheduler.Api.Models;
using LX.StaffScheduler.DAL;

namespace LX.StaffScheduler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _svc;

        public CityController(ICityService service)
        {
            _svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityModel>>> Get()
        {
            var result = await _svc.GetAllAsync();
            var cities = result.FromDTO();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetById(int id)
        {
            var city = await _svc.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult<CityDTO>> Post(CityDTO cityDTO)
        {
            if (cityDTO == null)
            {
                return BadRequest("City data is null");
            }
            try
            {
                var createdCity = await _svc.AddAsync(cityDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdCity.Id }, createdCity);
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
