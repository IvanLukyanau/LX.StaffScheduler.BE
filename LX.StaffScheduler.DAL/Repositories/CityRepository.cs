using LX.StaffScheduler.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.DAL.Repositories
{
    public class CityRepository: ICityRepository
    {
        private readonly Context _context;

        public CityRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            return await _context.Cities.FirstOrDefaultAsync();
        }

        public async Task AddAsync(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(City city)
        {
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(City city)
        {
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }
    }
}
