using LX.StaffScheduler.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.BLL.Services.Interfaces
{
    public interface ICityService: IService<CityDTO>
    {
        Task<bool> IsCityNameUniqueAsync(string name);
    }
}
