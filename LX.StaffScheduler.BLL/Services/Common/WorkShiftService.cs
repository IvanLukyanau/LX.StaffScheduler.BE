using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL;
using LX.StaffScheduler.DAL.Interfaces;

namespace LX.StaffScheduler.BLL.Services.Common
{
    public class WorkShiftService : IWorkShiftService
    {
        private readonly IWorkShiftRepository repository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUserContractRepository userContractRepository;


        public WorkShiftService(IWorkShiftRepository repository, IEmployeeRepository employeeRepositor, IUserContractRepository userContractRepository)
        {
            this.repository = repository;
            this.employeeRepository = employeeRepositor;
            this.userContractRepository = userContractRepository;
        }

        public async Task<WorkShiftDTO> AddAsync(WorkShiftDTO entity)
        {
            var workShift = entity.WorkShiftFromDTO();
            await repository.AddAsync(workShift);
            return workShift.WorkShiftToDTO();
        }

        public async Task<IEnumerable<WorkShiftExtendedDTO>> CreateWeekSchedule(int cafeId, DateOnly monday)
        {
            var readyWeekShift = new List<WorkShiftExtendedDTO>();
            var standartOpenCafeTime = new TimeOnly(10, 0);
            var standartCloseCafeTime = new TimeOnly(22, 0);
            var shifts = await repository.GetWeekWorkShifts(cafeId, monday);
            var shiftsList = shifts?.ToList();

            if (shiftsList.Count() == 0)
            {
                var cafeEmployees = (await employeeRepository.GetCafeEmployees(cafeId)).ToList();
                var cafeEmployeesIds = cafeEmployees.Select(e => e.Id).Distinct().ToList();
                var userContractsList = await userContractRepository.GetAllEmployeeContracts(cafeEmployeesIds);

                for (int i = 0; i < 7; i++)
                {
                    var currentDay = monday.AddDays(i);
                    var dayShifts = GetDayShifts(cafeEmployees, (List<UserContract>)userContractsList, currentDay, cafeId, standartOpenCafeTime, standartCloseCafeTime);

                    if (!dayShifts.Any())
                    {
                        dayShifts.Add(CreateEmptyShift(currentDay, cafeId, standartOpenCafeTime, standartCloseCafeTime));
                    }

                    readyWeekShift.AddRange(dayShifts);
                }

                readyWeekShift = await FillDayGaps(readyWeekShift, standartCloseCafeTime, cafeId);

                return readyWeekShift.OrderBy(s => s.ShiftDate)
                                   .ThenBy(s => s.StartTime)
                                   .ToList();
            }
            else
            {
                var cafeEmployees = (await employeeRepository.GetCafeEmployees(cafeId)).ToDictionary(e => e.Id, e => e);
                readyWeekShift.AddRange(ConvertShiftsToExtended(shiftsList.WorkShiftsToDTOs(), cafeEmployees));

                var allDaysOfWeek = Enumerable.Range(0, 7).Select(i => monday.AddDays(i)).ToList();
                var existingDays = shifts.Select(s => s.ShiftDate).Distinct().ToList();

                foreach (var day in allDaysOfWeek)
                {
                    if (!existingDays.Contains(day))
                    {
                        readyWeekShift.Add(CreateEmptyShift(day, cafeId, standartOpenCafeTime, standartCloseCafeTime));
                    }
                    else
                    {
                        AdjustShiftsTimes(readyWeekShift, day, standartOpenCafeTime, standartCloseCafeTime);
                    }
                }

                readyWeekShift = await FillDayGaps(readyWeekShift, standartCloseCafeTime, cafeId);

                return readyWeekShift.OrderBy(s => s.ShiftDate)
                                     .ThenBy(s => s.StartTime)
                                     .ToList();
            }
        }

        private List<WorkShiftExtendedDTO> GetDayShifts(List<Employee> cafeEmployees, List<UserContract> userContractsList, DateOnly currentDay, int cafeId, TimeOnly standartOpenCafeTime, TimeOnly standartCloseCafeTime)
        {
            var dayShifts = new List<WorkShiftExtendedDTO>();

            foreach (var cafeEmployee in cafeEmployees)
            {
                var employeeContracts = userContractsList
                    .Where(u => u.EmployeeId == cafeEmployee.Id && (int)currentDay.DayOfWeek == u.DayWeek)
                    .ToList();

                foreach (var employee in employeeContracts)
                {
                    if (employee.StartContractTime < standartOpenCafeTime)
                        employee.StartContractTime = standartOpenCafeTime;

                    if (employee.EndContractTime > standartCloseCafeTime)
                        employee.EndContractTime = standartCloseCafeTime;

                    var shift = new WorkShiftExtendedDTO

                    {
                        ShiftDate = currentDay,
                        StartTime = employee.StartContractTime,
                        EndTime = employee.EndContractTime,
                        CafeId = cafeId,
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = cafeEmployee.FirstName + " " + cafeEmployee.LastName
                    };
                    dayShifts.Add(shift);
                }
            }

            return dayShifts;
        }

        private WorkShiftExtendedDTO CreateEmptyShift(DateOnly day, int cafeId, TimeOnly standartOpenCafeTime, TimeOnly standartCloseCafeTime)
        {
            return new WorkShiftExtendedDTO
            {
                ShiftDate = day,
                StartTime = standartOpenCafeTime,
                EndTime = standartCloseCafeTime,
                CafeId = cafeId,
                EmployeeId = -1,
                EmployeeName = "No Employee"
            };
        }

        private void AdjustShiftsTimes(List<WorkShiftExtendedDTO> readyWeekShift, DateOnly day, TimeOnly standartOpenCafeTime, TimeOnly standartCloseCafeTime)
        {
            var dayShifts = readyWeekShift.Where(s => s.ShiftDate == day).ToList();

            foreach (var shift in dayShifts)
            {
                if (shift.StartTime < standartOpenCafeTime)
                    shift.StartTime = standartOpenCafeTime;

                if (shift.EndTime > standartCloseCafeTime)
                    shift.EndTime = standartCloseCafeTime;

                if (shift.EndTime < standartOpenCafeTime)
                    shift.EndTime = standartOpenCafeTime.AddHours(1);
            }
        }

        public async Task<List<WorkShiftExtendedDTO>> FillDayGaps(List<WorkShiftExtendedDTO> readyWeekShift, TimeOnly endShift, int cafeId)
        {
            var newReadyWeekShift = new List<WorkShiftExtendedDTO>(readyWeekShift);

            foreach (var dayShift in readyWeekShift)
            {
                var dayShifts = await repository.GetDayWorkShifts(cafeId, dayShift.ShiftDate);
                var dayShifftsList = dayShifts.ToList();

                if(dayShifftsList.Count() == 0)
                {
                    var temp = ConvertExtendedShiftsToNormal(readyWeekShift);
                    dayShifts = temp.Where(ws => ws.CafeId == cafeId && ws.ShiftDate == dayShift.ShiftDate).ToList();
                }
                var sortedShifts = dayShifts.OrderBy(s => s.StartTime).ToList();

                TimeOnly? currentStart = null;

                foreach (var shift in sortedShifts)
                {
                    if (currentStart.HasValue && shift.StartTime >= currentStart.Value.AddHours(1))
                    {
                        var newShift = new WorkShiftExtendedDTO
                        {
                            ShiftDate = dayShift.ShiftDate,
                            StartTime = currentStart.Value,
                            EndTime = shift.StartTime,
                            CafeId = cafeId,
                            EmployeeId = -1,
                            EmployeeName = "Free Slot"
                        };
                        newReadyWeekShift.Add(newShift);
                        currentStart = null;
                    }

                    if (shift.EndTime < endShift)
                    {
                        currentStart = shift.EndTime;
                    }
                }

                if (currentStart.HasValue && currentStart.Value < endShift)
                {
                    var newShift = new WorkShiftExtendedDTO
                    {
                        ShiftDate = dayShift.ShiftDate,
                        StartTime = currentStart.Value,
                        EndTime = endShift,
                        CafeId = cafeId,
                        EmployeeId = -1,
                        EmployeeName = "Free Slot"
                    };
                    newReadyWeekShift.Add(newShift);
                }
            }

            return newReadyWeekShift;
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

        public async Task<IEnumerable<WorkShiftDTO>> GetWeekWorkShiftss(int cafeId, DateOnly monday)
        {
            var shifts = await repository.GetWeekWorkShifts(cafeId, monday);

            return shifts?.WorkShiftsToDTOs();
        }

        private static List<WorkShift> ConvertExtendedShiftsToNormal(IEnumerable<WorkShiftExtendedDTO> extendedShifts)
        {
            var shifts = new List<WorkShift>();

            foreach (var extendedShift in extendedShifts)
            {
                var shift = new WorkShift
                {
                    Id = extendedShift.Id,
                    ShiftDate = extendedShift.ShiftDate,
                    StartTime = extendedShift.StartTime,
                    EndTime = extendedShift.EndTime,
                    CafeId = extendedShift.CafeId,
                    EmployeeId = extendedShift.EmployeeId
                };

                shifts.Add(shift);
            }

            return shifts;
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


        private List<WorkShiftExtendedDTO> ConvertShiftsToExtended(IEnumerable<WorkShiftDTO> shifts, Dictionary<int, Employee> cafeEmployees)
        {
            var extendedShifts = new List<WorkShiftExtendedDTO>();

            foreach (var shift in shifts)
            {
                var employeeName = cafeEmployees.ContainsKey(shift.EmployeeId)
                    ? cafeEmployees[shift.EmployeeId].FirstName + " " + cafeEmployees[shift.EmployeeId].LastName
                    : "Unknown";

                var extendedShift = new WorkShiftExtendedDTO
                {
                    Id = shift.Id,
                    ShiftDate = shift.ShiftDate,
                    StartTime = shift.StartTime,
                    EndTime = shift.EndTime,
                    CafeId = shift.CafeId,
                    EmployeeId = shift.EmployeeId,
                    EmployeeName = employeeName
                };

                extendedShifts.Add(extendedShift);
            }

            return extendedShifts;
        }

        public async Task<IEnumerable<DateOnly>> GetMondaysWorkShiftsAsync(int cafeId)
        {
            return  await repository.GetMondaysWorkShiftsAsync(cafeId);
        }
    }
}

