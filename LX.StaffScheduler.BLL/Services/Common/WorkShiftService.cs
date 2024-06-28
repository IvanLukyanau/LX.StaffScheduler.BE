using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL.Interfaces;

namespace LX.StaffScheduler.BLL.Services.Common
{
    public class WorkShiftService : IWorkShiftService
    {
        private readonly IWorkShiftRepository repository;


        public WorkShiftService(IWorkShiftRepository repository)
        {
            this.repository = repository;
        }

        public async Task<WorkShiftDTO> AddAsync(WorkShiftDTO entity)
        {
            var workShift = entity.WorkShiftFromDTO();
            await repository.AddAsync(workShift);
            return workShift.WorkShiftToDTO();
        }

        public async Task<List<WorkShiftDTO>> GetAllAsync()
        {
            var workShifts = await repository.GetAllAsync();
            return workShifts.Select(workShift => workShift.WorkShiftToDTO()).ToList();
        }

        public async Task<WorkShiftDTO?> GetByIdAsync(int id)
        {
            var workShift = await repository.GetByIdAsync(id);
            return workShift?.WorkShiftToDTO();
        }

        public async Task RemoveAsync(int id)
        {
            await repository.RemoveAsync(id);
        }

        public async Task UpdateAsync(WorkShiftDTO entity)
        {
            var workShift = await repository.GetByIdAsync(entity.Id);
            if (workShift != null)
            {
                workShift.ShiftDate = entity.ShiftDate;
                workShift.StartTime = entity.StartTime;
                workShift.EndTime = entity.EndTime;
                await repository.UpdateAsync(workShift);
            }
        }
    }
}

