

namespace LX.StaffScheduler.BLL.DTO
{
    public class EmployeeDTO : IDTO
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public DateOnly StartContractDate { get; set; }

        public DateOnly EndContractDate { get; set; }

        public int CafeId { get; set; }

    }
}