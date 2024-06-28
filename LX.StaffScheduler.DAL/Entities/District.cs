using System.ComponentModel.DataAnnotations;

namespace LX.StaffScheduler.DAL
{
    public class District
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int CityId { get; set; }
        //public City City { get; set; }
    }
}
