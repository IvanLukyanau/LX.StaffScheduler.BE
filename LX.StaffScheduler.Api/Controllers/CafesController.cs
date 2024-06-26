﻿using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LX.StaffScheduler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafesController : ControllerBase
    {
        private readonly ICafeService _svc;

        public CafesController(ICafeService service)
        {
            _svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CafeDTO>>> Get()
        {
            var result = await _svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CafeDTO>> GetById(int id)
        {
            var result = await _svc.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CafeDTO>> Post(CafeDTO cafe)
        {
            if (cafe == null)
            {
                return BadRequest("Cafe data is null");
            }
            try
            {
                var createdCafe = await _svc.AddAsync(cafe);
                return CreatedAtAction(nameof(GetById), new { id = createdCafe.Id }, createdCafe);
            }
            catch (Exception ex)
            {
                return Problem($"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CafeDTO cafeDTO)
        {
            try
            {
                var existingCafe = await _svc.GetByIdAsync(id);
                if (existingCafe == null)
                {
                    return NotFound("Cafe not found");
                }

                existingCafe.Name = cafeDTO.Name;
                existingCafe.AddressOfCafe = cafeDTO.AddressOfCafe;
                existingCafe.DistrictId = cafeDTO.DistrictId;

                await _svc.UpdateAsync(existingCafe);
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
                var cafe = await _svc.GetByIdAsync(id);
                if (cafe == null)
                {
                    return NotFound("Cafe not found");
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

