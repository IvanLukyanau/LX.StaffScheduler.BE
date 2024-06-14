using System.ComponentModel.DataAnnotations;

namespace LX.StaffScheduler.DAL
{
    public class City
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

    }
}
