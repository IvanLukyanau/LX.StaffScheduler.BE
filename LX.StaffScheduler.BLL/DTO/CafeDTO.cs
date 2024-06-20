using LX.StaffScheduler.DAL;

namespace LX.StaffScheduler.BLL.DTO
{
    public class CafeDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressOfCafe { get; set; }
        public int DistrictId { get; set; }

    }
}
