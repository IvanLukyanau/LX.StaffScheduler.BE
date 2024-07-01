using LX.StaffScheduler.BLL.DTO;

namespace LX.StaffScheduler.BLL.Services.Interfaces
{
    public interface IWorkShiftService : IService<WorkShiftDTO>
    {

        Task<IEnumerable<WorkShiftDTO>> GetWeekWorkShiftss(int cafeId, DateOnly day);

        Task<IEnumerable<WorkShiftExtendedDTO>> CreateWeekSchedule(int cafeId, DateOnly monday);

        Task<List<WorkShiftExtendedDTO>> FillDayGaps(List<WorkShiftExtendedDTO> readyWeekShift, TimeOnly endShift, int cafeId);

        Task<bool>IsCurrentWeekScheduleExists(int cafeId, DateOnly monday);
    }
}
