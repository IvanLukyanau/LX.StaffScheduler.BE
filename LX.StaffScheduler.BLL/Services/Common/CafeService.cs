using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL.Repositories;

namespace LX.StaffScheduler.BLL.Services.Common
{
    public class CafeService : ICafeService
    {
        private readonly CafeRepository repository;

        public CafeService(CafeRepository repository)
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
            var cafe = await repository.GetByIdAsync(id);
            if (cafe != null)
            {
                await repository.RemoveAsync(cafe);
            }
        }

        public async Task UpdateAsync(CafeDTO entity)
        {
            var cafe = entity.CafeFromDTO();
            await repository.UpdateAsync(cafe);
        }
    }
}
