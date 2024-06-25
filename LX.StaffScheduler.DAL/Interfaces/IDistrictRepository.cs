namespace LX.StaffScheduler.DAL.Interfaces
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task<List<District>> GetByCityIdAsync(int cityId);
    }
}
