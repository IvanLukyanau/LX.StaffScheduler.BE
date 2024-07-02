using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.DAL;

namespace LX.StaffScheduler.BLL.Services.Interfaces
{
    public interface IWorkShiftService : IService<WorkShiftDTO>
    {

        Task<IEnumerable<WorkShiftDTO>> GetWeekWorkShiftss(int cafeId, DateOnly day);

        Task<IEnumerable<WorkShiftExtendedDTO>> CreateWeekSchedule(int cafeId, DateOnly monday);

        Task<List<WorkShiftExtendedDTO>> FillDayGaps(List<WorkShiftExtendedDTO> readyWeekShift, TimeOnly endShift, int cafeId);

        Task<bool>IsCurrentWeekScheduleExists(int cafeId, DateOnly monday);
        Task<IEnumerable<DateOnly>> GetMondaysWorkShiftsAsync(int cafeId);

     Task<IEnumerable<WorkShift>> SaveWeekWorkShifts(IEnumerable<WorkShiftExtendedDTO> workShifts);
        Task<IEnumerable<WorkShift>> UpdateWeekWorkShifts(IEnumerable<WorkShiftExtendedDTO> workShifts);



    }
}
