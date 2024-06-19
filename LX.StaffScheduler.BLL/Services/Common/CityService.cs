﻿using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL.Interfaces;
using LX.StaffScheduler.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.BLL.Services.Common
{
    public class CityService: ICityService
    {
        private readonly ICityRepository repository;


        public CityService(ICityRepository repository)
        {
            this.repository = repository;
        }

         public async Task<CityDTO> AddAsync(CityDTO entity)
        {
            var city = entity.FromDTO();
            await repository.AddAsync(city); 
            return city.ToDTO();
        }

        public async Task<List<CityDTO>> GetAllAsync()
        {
            var cities = await repository.GetAllAsync();
            return cities.Select(city => city.ToDTO()).ToList();
        }

        public async Task<CityDTO?> GetByIdAsync(int id)
        {
            var city = await repository.GetByIdAsync(id);
            return city?.ToDTO();
        }

        public async Task RemoveAsync(CityDTO entity)
        {
            var city = entity.FromDTO();
            repository.RemoveAsync(city);
        }

        public async Task UpdateAsync(CityDTO entity)
        {
            var city = entity.FromDTO();
            await repository.UpdateAsync(city);
        }

    }
}