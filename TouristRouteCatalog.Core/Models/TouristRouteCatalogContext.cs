using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TouristRouteCatalog.Core.Models.Mapping;

namespace TouristRouteCatalog.Core.Models
{
    public partial class TouristRouteCatalogContext : DbContext
    {
        static TouristRouteCatalogContext()
        {
            Database.SetInitializer<TouristRouteCatalogContext>(null);
        }

        public TouristRouteCatalogContext()
            : base("Name=TouristRouteCatalogContext")
        {
        }

        public DbSet<Campsite> Campsites { get; set; }
        public DbSet<RouteDifficultyLevel> RouteDifficultyLevels { get; set; }
        public DbSet<RouteGeoPoint> RouteGeoPoints { get; set; }
        public DbSet<RouteImage> RouteImages { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WaterSource> WaterSources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CampsiteMap());
            modelBuilder.Configurations.Add(new RouteDifficultyLevelMap());
            modelBuilder.Configurations.Add(new RouteGeoPointMap());
            modelBuilder.Configurations.Add(new RouteImageMap());
            modelBuilder.Configurations.Add(new RouteMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new WaterSourceMap());
        }
    }
}
