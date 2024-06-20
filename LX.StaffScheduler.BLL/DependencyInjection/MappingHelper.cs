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
        //City mappers
        public static City CityFromDTO(this CityDTO CityDTO)
        {
            return new City
            {
                Id = CityDTO.Id,
                Name = CityDTO.cityName
            };
        }

        public static CityDTO CityToDTO(this City City)
        {
            return new CityDTO
            {
                Id = City.Id,
                cityName = City.Name
            };
        }

        public static IEnumerable<City> CitiesFromDTOs(this IEnumerable<CityDTO> CityDTO)
        {
            return CityDTO.Select(CityFromDTO);
        }

        public static IEnumerable<CityDTO> CitiesToDTOs(this IEnumerable<City> City)
        {
            return City.Select(CityToDTO);
        }

        //Cafe mappers
        public static Cafe CafeFromDTO(this CafeDTO CafeDTO)
        {
            return new Cafe
            {
                Id = CafeDTO.Id,
                Name = CafeDTO.Name,
                AddressOfCafe = CafeDTO.AddressOfCafe,
                DistrictId = CafeDTO.DistrictId,
                District = CafeDTO.District
            };
        }

        public static CafeDTO CafeToDTO(this Cafe Cafe)
        {
            return new CafeDTO
            {
                Id = Cafe.Id,
                Name = Cafe.Name,
                AddressOfCafe = Cafe.AddressOfCafe,
                DistrictId = Cafe.DistrictId,
                District = Cafe.District
            };
        }

        public static IEnumerable<Cafe> CafesFromDTOs(this IEnumerable<CafeDTO> CafeDTO)
        {
            return CafeDTO.Select(CafeFromDTO);
        }

        public static IEnumerable<CafeDTO> CafesToDTOs(this IEnumerable<Cafe> Cafe)
        {
            return Cafe.Select(CafeToDTO);
        }

    }
}
