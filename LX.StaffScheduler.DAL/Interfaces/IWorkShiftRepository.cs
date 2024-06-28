namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IWorkShiftRepository : IRepository<WorkShift>
    {
        Task<IEnumerable<DateOnly>> GetMondaysWorkShiftsAsync(int cafeId);
    }
}
