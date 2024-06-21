using LX.StaffScheduler.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LX.StaffScheduler.DAL.Repositories
{
    public class UserContractRepository : IUserContractRepository
    {
        private readonly Context _context;

        public UserContractRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<UserContract>> GetAllAsync()
        {
            return await _context.UserContracts.ToListAsync();
        }

        public async Task<UserContract?> GetByIdAsync(int id)
        {
            return await _context.UserContracts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(UserContract userContract)
        {
            _context.UserContracts.Add(userContract);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserContract userContract)
        {
            _context.UserContracts.Update(userContract);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var userContract = await _context.UserContracts.FirstOrDefaultAsync(x => x.Id == id);
            _context.UserContracts.Remove(userContract);
            await _context.SaveChangesAsync();
        }
    }
}
