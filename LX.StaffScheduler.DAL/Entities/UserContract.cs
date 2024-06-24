namespace LX.StaffScheduler.DAL
{
    public class UserContract
    {
        public int Id { get; set; }
        public int DayWeek { get; set; }
        public DateOnly StartContractTime { get; set; }
        public DateOnly EndContractTime { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
