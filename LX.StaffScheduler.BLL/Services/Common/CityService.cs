using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL.Interfaces;

namespace LX.StaffScheduler.BLL.Services.Common
{
    public class CityService : ICityService
    {
        private readonly ICityRepository repository;


        public CityService(ICityRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CityDTO> AddAsync(CityDTO entity)
        {
            var city = entity.CityFromDTO();
            await repository.AddAsync(city); 
            return city.CityToDTO();
        }

        public async Task<List<CityDTO>> GetAllAsync()
        {
            var cities = await repository.GetAllAsync();
            return cities.Select(city => city.CityToDTO()).ToList();
        }

        public async Task<CityDTO?> GetByIdAsync(int id)
        {
            var city = await repository.GetByIdAsync(id);
            return city?.CityToDTO();
        }

        public async Task RemoveAsync(int id)
        {
           await repository.RemoveAsync(id);
        }

        public async Task UpdateAsync(CityDTO entity)
        {
            var city = await repository.GetByIdAsync(entity.Id);
            if (city != null)
            {
                city.Name = entity.Name;
                await repository.UpdateAsync(city);
            }
        }

        public async Task<bool> IsCityNameUniqueAsync(string name)
        {
            var cities = await repository.GetAllAsync();
            return !cities.Any(city => city.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
