namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IUserContractRepository : IRepository<UserContract>
    {

        Task RemoveAllEmployeeContractsAsync(int userId);

        Task<IEnumerable<UserContract>> GetAllEmployeeContracts(int userId);
    }
}
