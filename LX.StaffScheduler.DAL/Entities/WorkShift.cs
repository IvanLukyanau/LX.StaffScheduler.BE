namespace LX.StaffScheduler.DAL
{
    public class WorkShift
    {
        public int Id { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int CafeId { get; set; }
        public int EmployeeId { get; set; }

        public Cafe Cafe { get; set; }

        public Employee Employee { get; set; }


    }
}
