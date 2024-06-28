using LX.StaffScheduler.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LX.StaffScheduler.DAL.Repositories
{
    public class CafeRepository : ICafeRepository
    {
        private readonly Context _context;

        public CafeRepository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(Cafe cafe)
        {
            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cafe>> GetAllAsync()
        {
            return await _context.Cafes.ToListAsync();
        }

        public async Task<Cafe?> GetByIdAsync(int id)
        {
            return await _context.Cafes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var cafe = await _context.Cafes.FirstOrDefaultAsync(x => x.Id == id);
            _context.Cafes.Remove(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cafe cafe)
        {
            _context.Cafes.Update(cafe);
            await _context.SaveChangesAsync();
        }
    }
}
