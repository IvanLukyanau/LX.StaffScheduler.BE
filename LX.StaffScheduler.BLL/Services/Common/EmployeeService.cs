using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL.Interfaces;

namespace LX.StaffScheduler.BLL.Services.Common
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<EmployeeDTO> AddAsync(EmployeeDTO entity)
        {
            var employee = entity.EmployeeFromDTO();
            await repository.AddAsync(employee);
            return employee.EmployeeToDTO();
        }

        public async Task<List<EmployeeDTO>> GetAllAsync()
        {
            var employees = await repository.GetAllAsync();
            return employees.Select(employee => employee.EmployeeToDTO()).ToList();
        }

        public async Task<EmployeeDTO?> GetByIdAsync(int id)
        {
            var employee = await repository.GetByIdAsync(id);
            return employee?.EmployeeToDTO();
        }

        public async Task RemoveAsync(int id)
        {
            await repository.RemoveAsync(id);
        }

        public async Task UpdateAsync(EmployeeDTO entity)
        {
            var employee = entity.EmployeeFromDTO();
            await repository.UpdateAsync(employee);
        }
    }
}