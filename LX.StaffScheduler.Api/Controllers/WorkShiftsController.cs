using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LX.StaffScheduler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkShiftsController : ControllerBase
    {
        private readonly IWorkShiftService _svc;

        public WorkShiftsController(IWorkShiftService service)
        {
            _svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkShiftDTO>>> Get()
        {
            var result = await _svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkShiftDTO>> GetById(int id)
        {
            var workShift = await _svc.GetByIdAsync(id);
            if (workShift == null)
            {
                return NotFound();
            }
            return Ok(workShift);
        }

        [HttpPost]
        public async Task<ActionResult<WorkShiftDTO>> Post(WorkShiftDTO workShiftDTO)
        {
            if (workShiftDTO == null)
            {
                return BadRequest("Work shift data is null");
            }
            try
            {
                var createdWorkShift = await _svc.AddAsync(workShiftDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdWorkShift.Id }, createdWorkShift);
            }
            catch (Exception ex)
            {
                return Problem($"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] WorkShiftDTO workShiftDTO)
        {
            try
            {
                var existingWorkShift = await _svc.GetByIdAsync(id);
                if (existingWorkShift == null)
                {
                    return NotFound("Work shift not found");
                }

                existingWorkShift.ShiftDate = workShiftDTO.ShiftDate;
                existingWorkShift.StartTime = workShiftDTO.StartTime;
                existingWorkShift.EndTime = workShiftDTO.EndTime;
                existingWorkShift.CafeId = workShiftDTO.CafeId;
                existingWorkShift.EmployeeId = workShiftDTO.EmployeeId;

                await _svc.UpdateAsync(existingWorkShift);
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
                var workShift = await _svc.GetByIdAsync(id);
                if (workShift == null)
                {
                    return NotFound("Work shift not found");
                }

                await _svc.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Internal server error: {ex.Message}");
            }
        }

        
        [HttpGet("/cafe/{cafeId}")]
        public async Task<IActionResult> GetWeekWorkShifts(int cafeId, [FromBody] DateOnly day )
        {
            try
            {
                var response = await _svc.GetWeekWorkShiftss(cafeId, day);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Problem($"Iternal server error: {ex.Message}");
            }
        }

        [HttpPost("/cafe/{cafeId}")]
        public async Task<IActionResult> PostWeekSchedule(int cafeId, [FromBody] DateOnly day )
        {
            try
            {
                var response = await _svc.GetWeekWorkShiftss(cafeId, day);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Problem($"Iternal server error: {ex.Message}");
            }

          
        }
    }
}