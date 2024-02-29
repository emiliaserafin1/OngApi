using ongApi.Entities;
using ongApi.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ongApi.Models.Dtos
{
    public class CreateAndUpdateActivityDto
    {
        public string Name { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public ActivityState State { get; set; }
        public int JefeId { get; set; }
        public string ImgUrl { get; set; }
        public List<int> SelectedMaterialIds { get; set; }
        public List<int> MaterialQuantities { get; set; }

    }
}
