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
                existingUserContract.EmployeeId = userContractDTO.EmployeeId;

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

        [HttpPost("user/{userId}/bulk")]
        public async Task<IActionResult> BulkContracts(int userId, [FromBody] IEnumerable<UserContractDTO> weekContract)
        {
            try
            {
                var response = await _svc.BulkContracts(userId, weekContract);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem($"Save week contracts error: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}/bulk")]
        public async Task<IActionResult> BulkRecieveContracts(int userId)
        {
            try
            {
                var result = await _svc.GetEmployeesContracts(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem($"Problem with getting employee week contracts: {ex.Message}");
            }
        }


    }
}
