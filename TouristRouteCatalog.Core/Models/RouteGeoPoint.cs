using System;
using System.Collections.Generic;
using TouristRouteCatalog.Core.Model;

namespace TouristRouteCatalog.Core.Models
{
    public partial class RouteGeoPoint : IModel
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int OrderIndex { get; set; }
        public virtual Route Route { get; set; }
    }
}
