﻿using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<CityDTO>>> Get()
        {
            var result = await _svc.GetAllAsync();
            var cities = result.CitiesFromDTOs();
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
                return Problem($"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CityDTO cityDTO)
        {
            try
            {
                var existingCity = await _svc.GetByIdAsync(id);
                if (existingCity == null)
                {
                    return NotFound("City not found");
                }

                existingCity.Name = cityDTO.Name;

                await _svc.UpdateAsync(existingCity);
                return Ok(existingCity);
            }
            catch (Exception ex)
            {
                return Problem($"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var city = await _svc.GetByIdAsync(id);
                if (city == null)
                {
                    return NotFound("City not found");
                }

                await _svc.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Internal server error: {ex.Message}");
            }
        }
    }
}
