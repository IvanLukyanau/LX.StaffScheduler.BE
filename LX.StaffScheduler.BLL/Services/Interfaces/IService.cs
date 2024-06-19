using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.BLL.Services.Interfaces
{
    public interface IService<T> where T : IDTO
    {
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T t);
        Task<T> AddAsync(T t);
        Task RemoveAsync(T t);
        Task<T?> GetByIdAsync(int id);
    }
}
