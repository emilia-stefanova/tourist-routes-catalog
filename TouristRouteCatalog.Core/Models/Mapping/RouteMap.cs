using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TouristRouteCatalog.Core.Models.Mapping
{
    public class RouteMap : EntityTypeConfiguration<Route>
    {
        public RouteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Seasons)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Routes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatorId).HasColumnName("CreatorId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DifficultyLevel).HasColumnName("DifficultyLevel");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Seasons).HasColumnName("Seasons");
            this.Property(t => t.PublicTransport).HasColumnName("PublicTransport");

            // Relationships
            this.HasMany(t => t.WaterSources)
                .WithMany(t => t.Routes)
                .Map(m =>
                    {
                        m.ToTable("RoutesWaterSources");
                        m.MapLeftKey("RouteId");
                        m.MapRightKey("WaterSourceId");
                    });

            this.HasRequired(t => t.RouteDifficultyLevel)
                .WithMany(t => t.Routes)
                .HasForeignKey(d => d.DifficultyLevel);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Routes)
                .HasForeignKey(d => d.CreatorId);

        }
    }
}
