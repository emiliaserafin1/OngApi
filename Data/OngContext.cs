using Microsoft.EntityFrameworkCore;
using ongApi.Entities;

namespace ongApi.Data
{
    public class OngContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ActivityMaterial> ActivityMaterials { get; set; }

        public OngContext(DbContextOptions<OngContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.ActivitiesAsParticipant)
            .WithMany(a => a.Users)
            .UsingEntity(j =>
            {
                j.ToTable("UserActivity");
            });

            modelBuilder.Entity<User>()
                .HasMany(u => u.ActivitiesAsJefe)
                .WithOne(a => a.Jefe)
                .HasForeignKey(a => a.JefeId);

            modelBuilder.Entity<ActivityMaterial>()
                .HasKey(am => new { am.ActivityId, am.MaterialId });

            modelBuilder.Entity<ActivityMaterial>()
                .HasOne(am => am.Activity)
                .WithMany(a => a.ActivityMaterials)
                .HasForeignKey(am => am.ActivityId);

            modelBuilder.Entity<ActivityMaterial>()
                .HasOne(am => am.Material)
                .WithMany(m => m.ActivityMaterials)
                .HasForeignKey(am => am.MaterialId);
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
