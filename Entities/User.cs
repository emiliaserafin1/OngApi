using ongApi.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ongApi.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string DNI { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRole Role { get; set; } = UserRole.Volunteer;
        public ICollection<Activity> ActivitiesAsParticipant { get; set; }
        public ICollection<Activity> ActivitiesAsJefe { get; set; }
    }
}
