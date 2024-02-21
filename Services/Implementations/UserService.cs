using Microsoft.EntityFrameworkCore;
using ongApi.Data;
using ongApi.Entities;
using ongApi.Models.Dtos;
using ongApi.Models.Enum;
using ongApi.Services.Interfaces;

namespace ongApi.Services.Implementations
{
    public class UserService:IUserService
    {
        private readonly OngContext _context;
        public UserService( OngContext context) 
        {
            _context = context;
        }

        public User? ValidateUser(AuthenticationRequestBodyDto authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Email == authRequestBody.Email && p.Password == authRequestBody.Password);
        }

        public List<GetUserDto> GetAllUsers()
        {
            return _context.Users.Select(u => new GetUserDto()
            {
                Name = u.Name,
                LastName = u.LastName,
                Email = u.Email,
                Address = u.Address,
                Phone = u.Phone,
                DNI = u.DNI,
                BirthDate = u.BirthDate,
                Role = u.Role
            }).ToList();    
        }
        public GetUserDto? GetUserById(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            if (user is not null)
            {
                return new GetUserDto()
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    Phone = user.Phone,
                    DNI = user.DNI,
                    BirthDate = user.BirthDate,
                    Role = user.Role
                };
            }
            return null;
        }
        public void CreateUser(CreateAndUpdateUserDto dto)
        {
            var newUser = new User()
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                Address = dto.Address,
                Phone = dto.Phone,
                DNI = dto.DNI,
                BirthDate = dto.BirthDate,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        public void UpdateUser(CreateAndUpdateUserDto dto, int userId)
        {
            User userToUpdate = _context.Users.First(u => u.Id == userId);
            userToUpdate.Name = dto.Name;
            userToUpdate.Email = dto.Email;
            userToUpdate.LastName = dto.LastName;
            userToUpdate.Password = dto.Password;
            userToUpdate.Address = dto.Address;
            userToUpdate.Phone = dto.Phone;
            userToUpdate.DNI = dto.DNI;
            userToUpdate.BirthDate = dto.BirthDate;
            _context.SaveChanges();
        }
        public void DeleteUser(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user is not null && user.Role != UserRole.Admin)
            {
                _context.Users.Remove(_context.Users.Single(u => u.Id == userId));
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("No se puede eliminar a un Administrador");
            }
        }
        public bool CheckIfUserExists(int userId)
        {
            User? user = _context.Users.FirstOrDefault(user => user.Id == userId);
            return user != null;
        }
        public void RegisterForActivity(int userId, int activityId)
        {
            try
            {
                // Obtener el usuario y la actividad de la base de datos
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                var activity = _context.Activities
                    .Include(a => a.Users) // Asegúrate de incluir la colección de usuarios asociados a la actividad
                    .FirstOrDefault(a => a.Id == activityId);

                if (user == null || activity == null)
                {
                    throw new Exception("User or activity not found");
                }

                if (!activity.Users.Contains(user))
                {
                    activity.Users.Add(user);
                    activity.VolunteerCount = activity.Users.Count;
                } else
                {
                    throw new Exception("El usuario ya esta registrado en esta actividad");
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error registering for activity", ex);
            }
        }
    
        public ICollection<Activity> GetUserActivities(int userId)
        {
            var userActivities = _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.ActivitiesAsParticipant)
                .ToList();
            return userActivities;
        }
    }
}
