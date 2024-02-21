namespace ongApi.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ActivityMaterial> ActivityMaterials { get; set; }
    }
}
