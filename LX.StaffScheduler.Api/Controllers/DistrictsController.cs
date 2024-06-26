﻿using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LX.StaffScheduler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IDistrictService _svc;

        public DistrictsController(IDistrictService service)
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

            bool isUnique = await _svc.IsDistrictNameUniqueAsync(districtDTO.Name, districtDTO.CityId);
            if (!isUnique)
            {
                return BadRequest("District name already exists for this city");
            }

            try
            {
                var createdDistrict = await _svc.AddAsync(districtDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdDistrict.Id }, createdDistrict);
            }
            catch (Exception ex)
            {
                return Problem($"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DistrictDTO districtDTO)
        {
            bool isUnique = await _svc.IsDistrictNameUniqueAsync(districtDTO.Name, districtDTO.CityId);
            if (!isUnique)
            {
                return BadRequest("District name already exists for this city");
            }
            try
            {
                var existingDistrict = await _svc.GetByIdAsync(id);
                if (existingDistrict == null)
                {
                    return NotFound("District not found");
                }

                existingDistrict.Name = districtDTO.Name;
                existingDistrict.CityId = districtDTO.CityId;

                await _svc.UpdateAsync(existingDistrict);
                return NoContent();
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
                var district = await _svc.GetByIdAsync(id);
                if (district == null)
                {
                    return NotFound("District not found");
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

