using ongApi.Entities;
using ongApi.Models.Dtos;

namespace ongApi.Services.Interfaces
{
    public interface IUserService
    {
        User? ValidateUser(AuthenticationRequestBodyDto authRequestBody);
        List<GetUserDto> GetAllUsers();
        GetUserDto? GetUserById(int userId);
        void CreateUser(CreateAndUpdateUserDto dto);
        void UpdateUser(CreateAndUpdateUserDto dto, int userId);
        void DeleteUser(int userId);
        bool CheckIfUserExists(int userId);
        void RegisterForActivity(int userId, int activityId);
        ICollection<Activity> GetUserActivities(int userId);
    }
}
