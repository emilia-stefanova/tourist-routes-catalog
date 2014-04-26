using System;
using System.Collections.Generic;
using TouristRouteCatalog.Core.Model;

namespace TouristRouteCatalog.Core.Models
{
    public partial class WaterSource : IModel
    {
        public WaterSource()
        {
            this.Routes = new List<Route>();
        }

        public int Id { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
