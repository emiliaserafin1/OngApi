using ongApi.Data;
using ongApi.Entities;
using ongApi.Models.Dtos;
using ongApi.Services.Interfaces;

namespace ongApi.Services.Implementations
{
    public class MaterialService : IMaterialService
    {
        private readonly OngContext _context;
        public MaterialService(OngContext context)
        {
            _context = context;
        }
        public List<Material> GetAllMaterials()
        {
            return _context.Materials.ToList();   
        }
        public Material? GetMaterialById(int materialId)
        {
            Material? material = _context.Materials.SingleOrDefault(m => m.Id == materialId);
            if (material is not null)
            {
                return material;
            }
            return null;
        }
        public void CreateMaterial(CreateAndUpdateMaterialDto dto)
        {
            var newMaterial = new Material()
            { 
                Name = dto.Name
            };
            
            _context.Materials.Add(newMaterial);
            _context.SaveChanges();
        }
        public void UpdateMaterial(CreateAndUpdateMaterialDto dto, int materialId)
        {
            Material? materialToUpdate = _context.Materials.SingleOrDefault(m => m.Id == materialId);
            if (materialToUpdate is not null)
            {
                materialToUpdate.Name = dto.Name;
                _context.SaveChanges();
            }
        }
        public void DeleteMaterial(int materialId)
        {
            var material = _context.Materials.SingleOrDefault(m => m.Id == materialId);
            if (material is not null)
            {
                _context.Materials.Remove(material);
                _context.SaveChanges();
            }
        }
    }
}
