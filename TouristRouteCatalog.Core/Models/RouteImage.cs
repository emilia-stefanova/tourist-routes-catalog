using System;
using System.Collections.Generic;

namespace TouristRouteCatalog.Core.Models
{
    public partial class RouteImage
    {
        public int Id { get; set; }
        public string ImageLocation { get; set; }
        public int RouteId { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Route Route { get; set; }
    }
}
