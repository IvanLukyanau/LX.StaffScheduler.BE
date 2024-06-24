namespace LX.StaffScheduler.BLL.DTO
{
    public class WorkShiftDTO : IDTO
    {
        public int Id { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int CafeId { get; set; }
        public int EmployeeId { get; set; }
    }
}
