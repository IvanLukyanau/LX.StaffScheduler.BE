using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LX.StaffScheduler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContractsController : ControllerBase
    {
        private readonly IUserContractService _svc;

        public UserContractsController(IUserContractService service)
        {
            _svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserContractDTO>>> Get()
        {
            var result = await _svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserContractDTO>> GetById(int id)
        {
            var userContract = await _svc.GetByIdAsync(id);
            if (userContract == null)
            {
                return NotFound();
            }
            return Ok(userContract);
        }

        [HttpPost]
        public async Task<ActionResult<UserContractDTO>> Post(UserContractDTO userContractDTO)
        {
            if (userContractDTO == null)
            {
                return BadRequest("User contract data is null");
            }
            try
            {
                var createdUserContract = await _svc.AddAsync(userContractDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdUserContract.Id }, createdUserContract);
            }
            catch (Exception ex)
            {
                return Problem($"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserContractDTO userContractDTO)
        {
            try
            {
                var existingUserContract = await _svc.GetByIdAsync(id);
                if (existingUserContract == null)
                {
                    return NotFound("User contract not found");
                }

                existingUserContract.DayWeek = userContractDTO.DayWeek;
                existingUserContract.StartContractTime = userContractDTO.StartContractTime;
                existingUserContract.EndContractTime = userContractDTO.EndContractTime;

                await _svc.UpdateAsync(existingUserContract);
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
                var userContract = await _svc.GetByIdAsync(id);
                if (userContract == null)
                {
                    return NotFound("User contract not found");
                }

                await _svc.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("bulk-week-contract-schedule/{id}")]
        public async Task<IActionResult> PostContractsWeekSchedule(int userId, IEnumerable<UserContractDTO> weekContract)
        {
            try
            {
                _svc.BulkWeekOfEmployeeUserContracts(userId, weekContract);
                return Ok(weekContract);
            }
            catch (Exception ex)
            {
                return Problem($"Save week contracts error: {ex.Message}");
            }
        }

        [HttpGet("weekly-contracts/{id}")]
        public async Task<IActionResult> GetContractsWeekSchedule(int userId)
        {
            try
            {
                var result = _svc.GetAllEmployeeContracts(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem($"Problem with getting employee week contracts: {ex.Message}");
            }
        }


    }
}
