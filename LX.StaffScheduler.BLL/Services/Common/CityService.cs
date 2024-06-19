using LX.StaffScheduler.BLL.DependencyInjection;
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

        public void Add(CityDTO entity)
        {
            repository.Add(entity.FromDTO());
        }

        public async Task<IEnumerable<CityDTO>> GetAll()
        {
            var cities = await Task.Run(() => repository.GetAll());
            return cities.Select(city => city.ToDTO());
        }

        public CityDTO? GetById(int id)
        {
            return repository.GetById(id)?.ToDTO();
        }

        public void Remove(CityDTO entity)
        {
            repository.Remove(entity.FromDTO());
        }

        public void Update(CityDTO entity)
        {
            repository.Update(entity.FromDTO());
        }
    }
}
