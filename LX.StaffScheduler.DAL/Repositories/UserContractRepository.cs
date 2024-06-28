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

        public async Task RemoveAllEmplContractsAsync(int userId)
        {
            var contractsToRemove = _context.UserContracts
                .Where(x => x.EmployeeId == userId);


            _context.UserContracts.RemoveRange(contractsToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserContract>> GetAllEmployeeContracts(int userId)
        {
            var contracts = await _context.UserContracts
                .Where(x => x.EmployeeId == userId)
                .ToListAsync();

            return contracts;
        }

        public async Task<IEnumerable<UserContract>> AddRangeAsync(IEnumerable<UserContract> contracts)
        {
            await _context.UserContracts.AddRangeAsync(contracts);
            await _context.SaveChangesAsync();

            var employeeIds = contracts.Select(c => c.EmployeeId).Distinct().ToList();

            var addedContracts = await _context.UserContracts
            .Where(c => employeeIds.Contains(c.EmployeeId))
            .ToListAsync();

            return addedContracts;
        }

        public async Task<IEnumerable<UserContract>> GetAllEmployeeContracts(List<int> userIds)
        {
            return await _context.UserContracts
                .Where(x => userIds.Contains(x.EmployeeId))
                .ToListAsync();

            /* N+1 problem
            select * from UserContracts as UC
            where UC.EmployeeId in ('21', '22', ...)


            (select * from UserContracts as UC
            where UC.EmployeeId = '21' ) * userIds.Count()

            */
        }
    }
}

