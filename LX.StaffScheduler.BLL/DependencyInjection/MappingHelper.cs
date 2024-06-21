﻿using LX.StaffScheduler.BLL.DTO;
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
        public static District FromDTO(this DistrictDTO districtDTO)
        {
            return new District
            {
                Id = districtDTO.Id,
                Name = districtDTO.Name,
                CityId = districtDTO.CityId,
            };
        }

        public static DistrictDTO ToDTO(this District district)
        {
            return new DistrictDTO
            {
                Id = district.Id,
                Name = district.Name,
                CityId = district.CityId,
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
        //Employee mappers
        public static Employee EmployeeFromDto(this EmployeeDTO EmployeeDTO)
        {
            return new Employee
            {
                Id = EmployeeDTO.Id,
                Login = EmployeeDTO.Login,
                Password = EmployeeDTO.Password,
                FirstName = EmployeeDTO.FirstName,
                LastName = EmployeeDTO.LastName,
                Phone = EmployeeDTO.Phone,
                StartContractDate = EmployeeDTO.StartContractDate,
                EndContractDate = EmployeeDTO.EndContractDate,
                CafeId = EmployeeDTO.CafeId
            };

            public static EmployeeDTO EmployeeToDTO(this Employee Employee)
            {
                return new EmployeeDTO
                {
                    Id = EmployeeDTO.Id,
                    Login = EmployeeDTO.Login,
                    Password = EmployeeDTO.Password,
                    FirstName = EmployeeDTO.FirstName,
                    LastName = EmployeeDTO.LastName,
                    Phone = EmployeeDTO.Phone,
                    StartContractDate = EmployeeDTO.StartContractDate,
                    EndContractDate = EmployeeDTO.EndContractDate,
                    CafeId = EmployeeDTO.CafeId
                };
            }

            public static IEnumerable<Employee> FromDTO(this IEnumerable<EmployeeDTO> employeeDTOs)
            {
                return employeeDTOs.Select(FromDTO);
            }

            public static IEnumerable<EmployeeDTO> ToDTO(this IEnumerable<Employee> employees)
            {
                return employees.Select(ToDTO);
            }
        }

    }
}
