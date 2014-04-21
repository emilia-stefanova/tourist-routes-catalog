using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TouristRouteCatalog.Core.Models.Mapping
{
    public class RouteGeoPointMap : EntityTypeConfiguration<RouteGeoPoint>
    {
        public RouteGeoPointMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RouteGeoPoints");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RouteId).HasColumnName("RouteId");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.OrderIndex).HasColumnName("OrderIndex");

            // Relationships
            this.HasRequired(t => t.Route)
                .WithMany(t => t.RouteGeoPoints)
                .HasForeignKey(d => d.RouteId);

        }
    }
}
