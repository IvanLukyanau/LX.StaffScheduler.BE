using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LX.StaffScheduler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _svc;

        public EmployeesController(IEmployeeService service)
        {
            _svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> Get()
        {
            var result = await _svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetById(int id)
        {
            var employees = await _svc.GetByIdAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> Post(EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                return BadRequest("Employee data is null ");
            }
            try
            {
                var createdEmployee = await _svc.AddAsync(employeeDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                return Problem($"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                var existingEmployee = await _svc.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound("Employee not found");
                }

                existingEmployee.Login = employeeDTO.Login;
                existingEmployee.Password = employeeDTO.Password;
                existingEmployee.FirstName = employeeDTO.FirstName;
                existingEmployee.LastName = employeeDTO.LastName;
                existingEmployee.Phone = employeeDTO.Phone;
                existingEmployee.StartContractDate = employeeDTO.StartContractDate;
                existingEmployee.EndContractDate = employeeDTO.EndContractDate;

                await _svc.UpdateAsync(existingEmployee);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Internal service error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = await _svc.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound("Employee not found");
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
