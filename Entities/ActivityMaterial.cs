namespace ongApi.Entities
{
    public class ActivityMaterial
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int Quantity { get; set; }
    }
}
