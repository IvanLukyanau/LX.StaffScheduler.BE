using LX.StaffScheduler.BLL.DTO;

namespace LX.StaffScheduler.BLL.Services.Interfaces
{
    public interface IEmployeeService : IService<EmployeeDTO>
    {
        Task<bool> IsEmployeePhoneUniqueAsync(string phone);
        Task<bool> IsEmployeeLoginUniqueAsync(string login);
    }
}
