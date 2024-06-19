using LX.StaffScheduler.DAL.Interfaces;
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

        public List<City> GetAll()
        {
            return _context.Cities.ToList();
        }

        public City? GetById(int id)
        {
            return _context.Cities.Find(id);
        }

        public void Add(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
        }

        public void Update(City city)
        {
            _context.Cities.Update(city);
            _context.SaveChanges();
        }

        public void Remove(City city)
        {
            _context.Cities.Remove(city);
            _context.SaveChanges();
        }
    }
}
