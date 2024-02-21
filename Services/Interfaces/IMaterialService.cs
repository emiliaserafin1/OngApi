using ongApi.Entities;
using ongApi.Models.Dtos;

namespace ongApi.Services.Interfaces
{
    public interface IMaterialService
    {
        List<Material> GetAllMaterials();
        Material? GetMaterialById(int materialId);
        void CreateMaterial(CreateAndUpdateMaterialDto dto);
        void UpdateMaterial(CreateAndUpdateMaterialDto dto, int materialId);
        void DeleteMaterial(int materialId);

    }
}
