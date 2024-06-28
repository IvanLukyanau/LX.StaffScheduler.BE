namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IUserContractRepository : IRepository<UserContract>
    {

        Task RemoveAllEmplContractsAsync(int userId);

        Task<IEnumerable<UserContract>> GetAllEmployeeContracts(int userId);
        Task<IEnumerable<UserContract>> GetAllEmployeeContracts(List<int> userIds);

        Task<IEnumerable<UserContract>> AddRangeAsync(IEnumerable<UserContract> contracts);
    }
}
