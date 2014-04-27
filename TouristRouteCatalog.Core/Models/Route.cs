using System;
using System.Collections.Generic;
using TouristRouteCatalog.Core.Model;

namespace TouristRouteCatalog.Core.Models
{
    public partial class Route
    {
        public Route()
        {
            this.RouteGeoPoints = new List<RouteGeoPoint>();
            this.RouteImages = new List<RouteImage>();
            this.Campsites = new List<Campsite>();
            this.WaterSources = new List<WaterSource>();
        }

        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; }
        public int DifficultyLevel { get; set; }
        public Nullable<long> Duration { get; set; }
        public string Description { get; set; }
        public string Seasons { get; set; }
        public string PublicTransport { get; set; }
        public virtual RouteDifficultyLevel RouteDifficultyLevel { get; set; }
        public virtual ICollection<RouteGeoPoint> RouteGeoPoints { get; set; }
        public virtual ICollection<RouteImage> RouteImages { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Campsite> Campsites { get; set; }
        public virtual ICollection<WaterSource> WaterSources { get; set; }
    }
}
