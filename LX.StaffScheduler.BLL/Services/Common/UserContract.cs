﻿using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
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

        public async Task BulkWeekOfEmployeeUserContracts(int userId, IEnumerable<UserContractDTO> weekContractsDTO)
        {

            if (weekContractsDTO != null)
            {
                await RemoveAllEmployeeContractsAsync(userId);

                foreach (var contractDTO in weekContractsDTO)
                {
                    contractDTO.EmployeeId = userId;
                    await AddAsync(contractDTO);
                }
            }
        }

        public async Task<List<UserContractDTO>> GetAllAsync()
        {
            var userContracts = await repository.GetAllAsync();
            return userContracts.Select(userContract => userContract.UserContractToDTO()).ToList();
        }
        public async Task<IEnumerable<UserContractDTO>> GetAllEmployeeContracts(int userId)
        {
            var allContracts = await repository.GetAllAsync();

            var allUserContracts = new List<UserContractDTO>();

            foreach (var contract in allContracts.Where(c => c.EmployeeId == userId))
            {
               allUserContracts.Add(contract.UserContractToDTO());
            }

            return allUserContracts;

        }

        public async Task<UserContractDTO?> GetByIdAsync(int id)
        {
            var userContract = await repository.GetByIdAsync(id);
            return userContract?.UserContractToDTO();
        }

        public async Task RemoveAllEmployeeContractsAsync(int userId)
        {
            var allContracts = await repository.GetAllAsync();
            foreach (var contract in allContracts.Where(c => c.EmployeeId == userId))
            {
                await repository.RemoveAsync(contract.Id);
            }
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
    }
}
