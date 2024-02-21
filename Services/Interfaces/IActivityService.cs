using ongApi.Entities;
using ongApi.Models.Dtos;

namespace ongApi.Services.Interfaces
{
    public interface IActivityService
    {
        List<Activity> GetAllActivities();
        Activity? GetActivityById(int activityId);
        void CreateActivity(CreateAndUpdateActivityDto dto);
        void UpdateActivity(CreateAndUpdateActivityDto dto, int activityId);
        void DeleteActivity(int activityId);
        List<User> GetActivityVolunteers(int activityId);
        List<GetMaterialsDto> GetActivityMaterials(int activityId);
    }
}
