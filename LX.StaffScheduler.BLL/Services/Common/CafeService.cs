﻿using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL.Interfaces;

namespace LX.StaffScheduler.BLL.Services.Common
{
    public class CafeService : ICafeService
    {
        private readonly ICafeRepository repository;

        public CafeService(ICafeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CafeDTO> AddAsync(CafeDTO entity)
        {
            var cafe = entity.CafeFromDTO();
            await repository.AddAsync(cafe);
            return cafe.CafeToDTO();
        }

        public async Task<List<CafeDTO>> GetAllAsync()
        {
            var cafes = await repository.GetAllAsync();
            return cafes.Select(cafe => cafe.CafeToDTO()).ToList();
        }

        public async Task<CafeDTO?> GetByIdAsync(int id)
        {
            var cafe = await repository.GetByIdAsync(id);
            return cafe?.CafeToDTO();
        }

        public async Task RemoveAsync(int id)
        {
           await repository.RemoveAsync(id);
        }

        public async Task UpdateAsync(CafeDTO entity)
        {
            var cafe = entity.CafeFromDTO();
            await repository.UpdateAsync(cafe);
        }
    }
}
