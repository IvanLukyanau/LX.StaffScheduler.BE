using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.BLL.DependencyInjection
{
    public static class MappingHelper
    {
        public static City FromDTO(this CityDTO CityDTO)
        {
            return new City
            {
                Id = CityDTO.Id,
                Name = CityDTO.Name
            };
        }

        public static CityDTO ToDTO(this City City)
        {
            return new CityDTO
            {
                Id = City.Id,
                Name = City.Name
            };
        }

        public static IEnumerable<City> FromDTO(this IEnumerable<CityDTO> CityDTO)
        {
            return CityDTO.Select(FromDTO);
        }

        public static IEnumerable<CityDTO> ToDTO(this IEnumerable<City> City)
        {
            return City.Select(ToDTO);
        }

    }
}
