using LX.StaffScheduler.BLL.DTO;

namespace LX.StaffScheduler.BLL.Services.Interfaces
{
    public interface IDistrictService : IService<DistrictDTO>
    {
        Task<List<DistrictDTO>> GetByCityIdAsync(int cityId);
        Task<bool> IsDistrictNameUniqueAsync(string nameDistrict, int idCity);
    }
}
