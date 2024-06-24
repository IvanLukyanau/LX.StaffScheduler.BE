using LX.StaffScheduler.BLL.DTO;

using System.ComponentModel;


namespace LX.StaffScheduler.BLL.Services.Interfaces
{
    public interface IUserContractService : IService<UserContractDTO>
    {
        Task BulkWeekOfEmployeeUserContracts(int userId, IEnumerable<UserContractDTO> weekContractsDTO);

        Task RemoveAllEmployeeContractsAsync(int userId);

        Task<IEnumerable<UserContractDTO>> GetAllEmployeeContracts(int userId);

    }
}
