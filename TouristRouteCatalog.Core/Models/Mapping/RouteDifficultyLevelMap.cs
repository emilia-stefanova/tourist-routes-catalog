using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TouristRouteCatalog.Core.Models.Mapping
{
    public class RouteDifficultyLevelMap : EntityTypeConfiguration<RouteDifficultyLevel>
    {
        public RouteDifficultyLevelMap()
        {
            // Primary Key
            this.HasKey(t => t.Difficulty);

            // Properties
            this.Property(t => t.Difficulty)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("RouteDifficultyLevels");
            this.Property(t => t.Difficulty).HasColumnName("Difficulty");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
