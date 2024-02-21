using ongApi.Models.Enum;

namespace ongApi.Entities
{
    public class Activity
    {
        public ICollection<ActivityMaterial> ActivityMaterials { get; set; } = new List<ActivityMaterial>();
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int VolunteerCount { get; set; } = 0;
        public ActivityState State { get; set; } = ActivityState.ConCupo;
        public ICollection<User> Users { get; set; }
        public int? JefeId { get; set; } 
        public User Jefe { get; set; }
    }
}
