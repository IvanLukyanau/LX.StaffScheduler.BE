using System.ComponentModel.DataAnnotations;

namespace LX.StaffScheduler.DAL
{
    public class Employee
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Login { get; set; }
        [StringLength(30)]
        public string Password { get; set; }
        [StringLength(20)]
        public string FirstName { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        public DateOnly StartContractDate { get; set; }
        public DateOnly EndContractDate { get; set; }
        public int CafeId { get; set; }

        public Cafe Cafe { get; set; }
        public IEnumerable<WorkShift> Workshifts { get; set; }

    }
}
