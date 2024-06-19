using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        void Update(T t);
        void Add(T t);
        void Remove(T t);
        T? GetById(int id);
    }
}
