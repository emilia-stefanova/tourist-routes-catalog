using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TouristRouteCatalog.Core.Models.Mapping
{
    public class CampsiteMap : EntityTypeConfiguration<Campsite>
    {
        public CampsiteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Campsites");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasMany(t => t.Routes)
                .WithMany(t => t.Campsites)
                .Map(m =>
                    {
                        m.ToTable("RoutesCampsites");
                        m.MapLeftKey("CampsiteId");
                        m.MapRightKey("RouteId");
                    });


        }
    }
}
