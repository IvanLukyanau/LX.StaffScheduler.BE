using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL.Interfaces;

namespace LX.StaffScheduler.BLL.Services.Common
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository repository;


        public DistrictService(IDistrictRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DistrictDTO> AddAsync(DistrictDTO entity)
        {
            var district = entity.DistrictFromDTO();
            await repository.AddAsync(district);
            return district.DistrictToDTO();
        }

        public async Task<List<DistrictDTO>> GetAllAsync()
        {
            var districts = await repository.GetAllAsync();
            return districts.Select(district => district.DistrictToDTO()).ToList();
        }

        public async Task<DistrictDTO?> GetByIdAsync(int id)
        {
            var district = await repository.GetByIdAsync(id);
            return district?.DistrictToDTO();
        }

        public async Task RemoveAsync(int id)
        {
            await repository.RemoveAsync(id);
        }

        public async Task UpdateAsync(DistrictDTO entity)
        {
            var district = await repository.GetByIdAsync(entity.Id);
            if (district != null)
            {
                district.Name = entity.Name;
                await repository.UpdateAsync(district);
            }
        }
        
    }
}
