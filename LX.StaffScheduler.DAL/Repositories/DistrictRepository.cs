using LX.StaffScheduler.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LX.StaffScheduler.DAL.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly Context _context;

        public DistrictRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<District>> GetAllAsync()
        {
            return await _context.Districts.ToListAsync();
        }

        public async Task<District?> GetByIdAsync(int id)
        {
            return await _context.Districts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(District district)
        {
            _context.Districts.Add(district);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(District district)
        {
            _context.Districts.Update(district);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var district = await _context.Districts.FirstOrDefaultAsync(x => x.Id == id);
            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();
        }
    }
}
