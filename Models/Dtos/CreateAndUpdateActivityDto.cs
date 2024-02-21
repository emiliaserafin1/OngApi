using ongApi.Entities;

namespace ongApi.Models.Dtos
{
    public class CreateAndUpdateActivityDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public int JefeId { get; set; }
        public List<int> SelectedMaterialIds { get; set; }
        public List<int> MaterialQuantities { get; set; }
    }
}
