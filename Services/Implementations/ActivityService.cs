using Microsoft.EntityFrameworkCore;
using ongApi.Data;
using ongApi.Entities;
using ongApi.Models.Dtos;
using ongApi.Services.Interfaces;

namespace ongApi.Services.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly OngContext _context;
        public ActivityService(OngContext context)
        {
            _context = context;
        }
        public List<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }
        public Activity? GetActivityById(int activityId)
        {
            Activity? activity = _context.Activities.SingleOrDefault(a => a.Id == activityId);
            if (activity is not null)
            {
                return activity;
            }
            return null;
        }
        public void CreateActivity(CreateAndUpdateActivityDto dto)
        {
            var newActivity = new Activity()
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
                Description = dto.Description,
                JefeId = dto.JefeId,
                ImgUrl = dto.ImgUrl
            };
            _context.Activities.Add(newActivity);
            _context.SaveChanges();

            for (int i = 0; i < dto.SelectedMaterialIds.Count; i++)
        {
            var materialId = dto.SelectedMaterialIds[i];
            var materialQuantity = dto.MaterialQuantities[i];

            var material = _context.Materials.FirstOrDefault(m => m.Id == materialId);
            if (material != null)
            {
                newActivity.ActivityMaterials.Add(new ActivityMaterial { Material = material, Quantity = materialQuantity });
            }
        }

            _context.SaveChanges();
        }
        public void UpdateActivity(CreateAndUpdateActivityDto dto, int activityId)
        {
            var activity = _context.Activities
                                  .Include(a => a.ActivityMaterials)
                                  .SingleOrDefault(a => a.Id == activityId);

            if (activity is not null)
            {
                activity.Name = dto.Name;
                activity.StartDate = dto.StartDate;
                activity.EndDate = dto.EndDate;
                activity.Location = dto.Location;
                activity.Description = dto.Description;
                activity.JefeId = dto.JefeId;

                // Eliminar todas las relaciones de materiales existentes
                activity.ActivityMaterials.Clear();

                // Agregar las nuevas relaciones de materiales seleccionadas
                for (int i = 0; i < dto.SelectedMaterialIds.Count; i++)
                {
                    var materialId = dto.SelectedMaterialIds[i];
                    var materialQuantity = dto.MaterialQuantities[i];

                    var material = _context.Materials.FirstOrDefault(m => m.Id == materialId);
                    if (material != null)
                    {
                        activity.ActivityMaterials.Add(new ActivityMaterial { Material = material, Quantity = materialQuantity });
                    }
                }

                _context.SaveChanges();
            }
        }



        public void DeleteActivity(int activityId)
        {
            var activity = _context.Activities.SingleOrDefault(a => a.Id == activityId);
            if (activity is not null)
            {
                _context.Activities.Remove(activity);
                _context.SaveChanges();
            }
        }
        public List<User> GetActivityVolunteers(int activityId)
        {
            var activity = _context.Activities
                                  .Include(a => a.Users) 
                                  .SingleOrDefault(a => a.Id == activityId);

            if (activity != null && activity.Users != null)
            {
                return activity.Users.ToList();
            }

            return new List<User>();
        }
        public List<GetMaterialsDto> GetActivityMaterials(int activityId)
        {
            var activityMaterials = _context.ActivityMaterials
                                            .Include(am => am.Material)
                                            .Where(am => am.ActivityId == activityId)
                                            .ToList();

            // Mapear los datos a un DTO para evitar la serialización circular
            var getMaterialDtos = activityMaterials.Select(am => new GetMaterialsDto
            {
                Name = am.Material.Name,
                Quantity = am.Quantity
            }).ToList();

            return getMaterialDtos;
        }

    }
}
