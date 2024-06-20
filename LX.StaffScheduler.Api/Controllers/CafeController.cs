using LX.StaffScheduler.Api.Models;
using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Common;
using LX.StaffScheduler.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LX.StaffScheduler.Api.Controllers
{
    public class CafeController : ControllerBase
    {
        private readonly ICafeService _svc;

        public CafeController(ICafeService service)
        {
            _svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CafeDTO>>> Get()
        {
            var result = await _svc.GetAllAsync();
            var cafes = result.CafesFromDTOs();
            return Ok(cafes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CafeDTO>> GetById(int id)
        {
            var cafe = await _svc.GetByIdAsync(id);
            if (cafe == null)
            {
                return NotFound();
            }
            return Ok(cafe);
        }

        [HttpPost]
        public async Task<ActionResult<CafeDTO>> Post(CafeDTO cafeDTO)
        {
            if (cafeDTO == null)
            {
                return BadRequest("City data is null");
            }
            try
            {
                var createdCafe = await _svc.AddAsync(cafeDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdCafe.Id }, createdCafe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async ActionResult<CafeDTO> Put(int id, [FromBody] CafeDTO cafeDTO)
        {
            if (cafeDTO == null || id != cafeDTO.Id)
            {
                return BadRequest("Invalid data or ID mismatch");
            }

            try
            {
                var updatedCafe = await _svc.UpdateAsync(cafeDTO);
                if (updatedCafe == null)
                {
                    return NotFound();
                }
                return Ok(updatedCafe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cafe = GetById(id);

            try
            {
                var result = await _svc.RemoveAsync(result.ca);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
