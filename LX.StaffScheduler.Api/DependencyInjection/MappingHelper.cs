using LX.StaffScheduler.Api.Models;
using LX.StaffScheduler.BLL.DTO;

namespace LX.StaffScheduler.Api.DependencyInjection
{
    public static class MappingHelper
    {
        public static CityModel FromDTO(this CityDTO CityDTO)
        {
            return new CityModel
            {
                Id = CityDTO.Id,
                Name = CityDTO.Name
            };
        }

        public static CityDTO ToDTO(this CityModel City)
        {
            return new CityDTO
            {
                Id = City.Id,
                Name = City.Name
            };
        }

        public static IEnumerable<CityModel> FromDTO(this IEnumerable<CityDTO> CityDTO)
        {
            return CityDTO.Select(FromDTO);
        }

        public static IEnumerable<CityDTO> ToDTO(this IEnumerable<CityModel> City)
        {
            return City.Select(ToDTO);
        }

    }
}
