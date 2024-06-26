using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL;
using LX.StaffScheduler.DAL.Interfaces;

namespace LX.StaffScheduler.BLL.Services.Common
{
    public class UserContractService : IUserContractService
    {
        private readonly IUserContractRepository repository;


        public UserContractService(IUserContractRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UserContractDTO> AddAsync(UserContractDTO entity)
        {
            var userContract = entity.UserContractFromDTO();
            await repository.AddAsync(userContract);
            return userContract.UserContractToDTO();
        }
        public async Task<IEnumerable<UserContractDTO>> AddAsync(IEnumerable<UserContractDTO> entities)
        {
            var userContracts = entities.UserContractsFromDTOs();
            await repository.AddRangeAsync(userContracts);
            return userContracts.UserContractsToDTOs();
        }
        public async Task<IEnumerable<UserContractDTO>> BulkContracts(int userId, IEnumerable<UserContractDTO> weekContractsDTO)
        {
            var contractResults = new List<UserContractDTO>();



            if (weekContractsDTO != null)
            {
                weekContractsDTO = weekContractsDTO.Select(contract =>
                {
                    contract.StartContractTime = CorrectTimeFormat(contract.StartContractTime);
                    contract.EndContractTime = CorrectTimeFormat(contract.EndContractTime);
                    return contract;
                });

                await RemoveAllEmployeeContractsAsync(userId);

                var mappedObjects = weekContractsDTO.UserContractsFromDTOs();
                var response = await repository.AddRangeAsync(mappedObjects);
                contractResults = response.UserContractsToDTOs().ToList();

            }
            return contractResults;
        }

        public async Task<List<UserContractDTO>> GetAllAsync()
        {
            var userContracts = await repository.GetAllAsync();
            return userContracts.Select(userContract => userContract.UserContractToDTO()).ToList();
        }
        public async Task<IEnumerable<UserContractDTO>> GetEmployeesContracts(int userId)
        {
            IEnumerable<UserContract> allEmployeeContracts = await repository.GetAllEmployeeContracts(userId);
            return allEmployeeContracts.UserContractsToDTOs();
        }

        public async Task<UserContractDTO?> GetByIdAsync(int id)
        {
            var userContract = await repository.GetByIdAsync(id);
            return userContract?.UserContractToDTO();
        }

        public async Task RemoveAllEmployeeContractsAsync(int userId)
        {
            await repository.RemoveAllEmplContractsAsync(userId);
        }

        public async Task RemoveAsync(int id)
        {
            await repository.RemoveAsync(id);
        }

        public async Task UpdateAsync(UserContractDTO entity)
        {
            var userContract = await repository.GetByIdAsync(entity.Id);
            if (userContract != null)
            {
                userContract.DayWeek = entity.DayWeek;
                userContract.StartContractTime = entity.StartContractTime;
                userContract.EndContractTime = entity.EndContractTime;
                await repository.UpdateAsync(userContract);
            }
        }
        private TimeOnly CorrectTimeFormat(TimeOnly time)
        {
            var parts = time.ToString().Split(':');

            if (parts.Length == 2)
            {
                time = TimeOnly.Parse($"{parts[0]}:{parts[1]}:00");
            }

            return time;
        }
    }
}
