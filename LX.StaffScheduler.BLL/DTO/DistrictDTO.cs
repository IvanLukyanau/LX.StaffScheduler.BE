using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.BLL.DTO
{
    public class DistrictDTO : IDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        //public CityDTO City { get; set; }

    }
}
