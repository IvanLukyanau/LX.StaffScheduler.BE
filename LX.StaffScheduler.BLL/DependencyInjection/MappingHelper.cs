using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.DAL;

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

        public static District FromDTO(this DistrictDTO districtDTO)
        {
            return new District
            {
                Id = districtDTO.Id,
                Name = districtDTO.Name,
                CityId = districtDTO.CityId,
                //City = districtDTO.City.FromDTO()

            };
        }

        public static DistrictDTO ToDTO(this District district)
        {
            return new DistrictDTO
            {
                Id = district.Id,
                Name = district.Name,
                CityId = district.CityId,
                //City = district.City.ToDTO()
                //City = new CityDTO
                //{
                //    Id = district.City.Id,
                //    Name = district.City.Name
                //}
            };
        }

        public static IEnumerable<District> FromDTO(this IEnumerable<DistrictDTO> districtDTOs)
        {
            return districtDTOs.Select(FromDTO);
        }

        public static IEnumerable<DistrictDTO> ToDTO(this IEnumerable<District> districts)
        {
            return districts.Select(ToDTO);
        }

    }
}
