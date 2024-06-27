using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL;
using LX.StaffScheduler.DAL.Interfaces;
using LX.StaffScheduler.DAL.Repositories;

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
            var standartCloseCafeTime = new TimeOnly(19, 0);

            var shifts = await repository.GetWeekWorkShifts(cafeId, monday);

            if (shifts == null)
            {
                var employeeList = await employeeRepository.GetCafeEmployees(cafeId);
                var cafeEmployees = employeeList.ToList();

                var cafeEmployeesIds = cafeEmployees.Select(e => e.Id).Distinct().ToList();
                var userContractsList = await userContractRepository.GetAllEmployeeContracts(cafeEmployeesIds);

                for (int i = 0; i < 7; i++)
                {
                    var currentDay = monday.AddDays(i);
                    var dayShifts = new List<WorkShiftExtendedDTO>();

                    foreach (var cafeEmployee in cafeEmployees)
                    {
                        var employeeContracts = userContractsList.Where(u => u.Id == cafeEmployee.Id && (int)currentDay.DayOfWeek == u.DayWeek).ToList();

                        foreach (var employee in employeeContracts)
                        {
                            if (employee.StartContractTime < standartOpenCafeTime)
                            {
                                employee.StartContractTime = standartOpenCafeTime;
                            }

                            if (employee.EndContractTime > standartCloseCafeTime)
                            {
                                employee.EndContractTime = standartCloseCafeTime;
                            }

                            var shift = new WorkShiftExtendedDTO
                            {
                                ShiftDate = currentDay,
                                StartTime = employee.StartContractTime,
                                EndTime = employee.EndContractTime,
                                CafeId = cafeId,
                                EmployeeId = employee.EmployeeId,
                                EmployeeName = cafeEmployee.FirstName + cafeEmployee.LastName
                            };
                            dayShifts.Add(shift);
                        }
                    }

                    if (!dayShifts.Any())
                    {
                        var emptyShift = new WorkShiftExtendedDTO
                        {
                            ShiftDate = currentDay,
                            StartTime = standartOpenCafeTime,
                            EndTime = standartCloseCafeTime,
                            CafeId = cafeId,
                            EmployeeId = -1,
                            EmployeeName = "No Employee"
                        };
                        dayShifts.Add(emptyShift);
                    }

                    readyWeekShift.AddRange(dayShifts);
                }

                return await FillDayGaps(readyWeekShift, standartCloseCafeTime, cafeId);
            }

            return readyWeekShift;
        }

        public async Task<List<WorkShiftExtendedDTO>> FillDayGaps(List<WorkShiftExtendedDTO> readyWeekShift, TimeOnly endShift, int cafeId)
        {
            foreach (var dayShift in readyWeekShift)
            {
                var dayShifts = await repository.GetDayWorkShifts(cafeId, dayShift.ShiftDate);

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
                        readyWeekShift.Add(newShift);
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
                    readyWeekShift.Add(newShift);
                }
            }
            return readyWeekShift;
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

