using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T t);
        Task AddAsync(T t);
        Task RemoveAsync(T t);
        Task<T?> GetByIdAsync(int id);
    }
}
