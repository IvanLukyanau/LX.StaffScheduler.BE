namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T t);
        Task AddAsync(T t);
        Task RemoveAsync(int id);
        Task<T?> GetByIdAsync(int id);
    }
}
