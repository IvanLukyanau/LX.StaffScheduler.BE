namespace LX.StaffScheduler.BLL.DTO
{
    public class UserContractDTO : IDTO
    {
        public int Id { get; set; }
        public int DayWeek { get; set; }
        public DateOnly StartContractTime { get; set; }
        public DateOnly EndContractTime { get; set; }
        public int EmployeeId { get; set; }
    }
}
