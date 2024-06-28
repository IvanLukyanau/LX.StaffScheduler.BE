namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetCafeEmployees(int cafeId);
    }
}