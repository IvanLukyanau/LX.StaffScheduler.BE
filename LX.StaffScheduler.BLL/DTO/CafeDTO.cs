using LX.StaffScheduler.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.BLL.DTO
{
    public class CafeDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressOfCafe { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }

    }
}
