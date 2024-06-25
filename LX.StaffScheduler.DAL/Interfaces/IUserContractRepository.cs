namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IUserContractRepository : IRepository<UserContract>
    {

        Task RemoveAllEmplContractsAsync(int userId);

        Task<IEnumerable<UserContract>> GetAllEmployeeContracts(int userId);
    }
}
