﻿using LX.StaffScheduler.BLL.DependencyInjection;
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

            var errors = new List<string>();

            bool isUniqueLogin = await _svc.IsEmployeeLoginUniqueAsync(employeeDTO.Login);
            if (!isUniqueLogin)
            {
                errors.Add("Login already exists");
            }

            bool isUniquePhone = await _svc.IsEmployeePhoneUniqueAsync(employeeDTO.Phone);
            if (!isUniquePhone)
            {
                errors.Add("Phone already exists");
            }

            if (errors.Any())
            {
                return BadRequest(string.Join("; ", errors));
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
            var errors = new List<string>();

            bool isUniqueLogin = await _svc.IsEmployeeChangeLoginUniqueAsync(employeeDTO.Id ,employeeDTO.Login);
            if (!isUniqueLogin)
            {
                errors.Add("Login already exists for another employee");
            }

            bool isUniquePhone = await _svc.IsEmployeePhoneChangeUniqueAsync(employeeDTO.Id, employeeDTO.Phone);
            if (!isUniquePhone)
            {
                errors.Add("Phone already exists for another employee");
            }

            if (errors.Any())
            {
                return BadRequest(string.Join("; ", errors));
            }

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
                existingEmployee.CafeId = employeeDTO.CafeId;

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
