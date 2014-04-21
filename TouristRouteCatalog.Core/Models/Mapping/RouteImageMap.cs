using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TouristRouteCatalog.Core.Models.Mapping
{
    public class RouteImageMap : EntityTypeConfiguration<RouteImage>
    {
        public RouteImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageLocation)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("RouteImages");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageLocation).HasColumnName("ImageLocation");
            this.Property(t => t.RouteId).HasColumnName("RouteId");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Route)
                .WithMany(t => t.RouteImages)
                .HasForeignKey(d => d.RouteId);

        }
    }
}
