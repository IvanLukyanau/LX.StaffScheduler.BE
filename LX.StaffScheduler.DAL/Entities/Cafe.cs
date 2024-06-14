using System.ComponentModel.DataAnnotations;

namespace LX.StaffScheduler.DAL
{
    public class Cafe
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string AddressOfCafe { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }

        public IEnumerable<WorkShift> Workshifts { get; set; }
        public IEnumerable<Employee> Employees { get; set; }


    }
}
