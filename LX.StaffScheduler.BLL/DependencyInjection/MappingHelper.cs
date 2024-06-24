using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.DAL;

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
                Name = CityDTO.Name
            };
        }

        public static CityDTO CityToDTO(this City City)
        {
            return new CityDTO
            {
                Id = City.Id,
                Name = City.Name
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
                DistrictId = CafeDTO.DistrictId
            };
        }

        public static CafeDTO CafeToDTO(this Cafe Cafe)
        {
            return new CafeDTO
            {
                Id = Cafe.Id,
                Name = Cafe.Name,
                AddressOfCafe = Cafe.AddressOfCafe,
                DistrictId = Cafe.DistrictId
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

        //District mappers

        public static District DistrictFromDTO(this DistrictDTO districtDTO)
        {
            return new District
            {
                Id = districtDTO.Id,
                Name = districtDTO.Name,
                CityId = districtDTO.CityId,
            };
        }

        public static DistrictDTO DistrictToDTO(this District district)
        {
            return new DistrictDTO
            {
                Id = district.Id,
                Name = district.Name,
                CityId = district.CityId,
            };
        }


        public static IEnumerable<District> DistrictsFromDTOs(this IEnumerable<DistrictDTO> DistrictDTO)
        {
            return DistrictDTO.Select(DistrictFromDTO);
        }

        public static IEnumerable<DistrictDTO> DistrictsToDTOs(this IEnumerable<District> District)
        {
            return District.Select(DistrictToDTO);
        }

        //UserContract mappers
        public static UserContract UserContractFromDTO(this UserContractDTO userContractDTO)
        {
            return new UserContract
            {
                Id = userContractDTO.Id,
                DayWeek = userContractDTO.DayWeek,
                StartContractTime = userContractDTO.StartContractTime,
                EndContractTime = userContractDTO.EndContractTime,
                EmployeeId = userContractDTO.EmployeeId,
            };
        }

        public static UserContractDTO UserContractToDTO(this UserContract userContract)
        {
            return new UserContractDTO
            {
                Id = userContract.Id,
                DayWeek = userContract.DayWeek,
                StartContractTime = userContract.StartContractTime,
                EndContractTime = userContract.EndContractTime,
                EmployeeId = userContract.EmployeeId,
            };
        }

        public static IEnumerable<UserContract> UserContractsFromDTOs(this IEnumerable<UserContractDTO> UserContractDTO)
        {
            return UserContractDTO.Select(UserContractFromDTO);
        }

        public static IEnumerable<UserContractDTO> UserContractsToDTOs(this IEnumerable<UserContract> UserContract)
        {
            return UserContract.Select(UserContractToDTO);
        }

    }
}
