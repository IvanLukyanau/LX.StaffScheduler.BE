namespace LX.StaffScheduler.DAL
{
    public class UserContract
    {
        public int Id { get; set; }
        public int DayWeek { get; set; }
        public TimeOnly StartContractTime { get; set; }
        public TimeOnly EndContractTime { get; set; }
        public int EmployeeId { get; set; }


    }
}
