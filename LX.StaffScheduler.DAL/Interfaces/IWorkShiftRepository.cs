namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IWorkShiftRepository : IRepository<WorkShift>
    {
        Task<IEnumerable<WorkShift>> GetWeekWorkShifts(int cafeId, DateOnly weekStartDate);
        Task<IEnumerable<WorkShift>> GetDayWorkShifts(int cafeId, DateOnly weekStartDate);
    }
}
