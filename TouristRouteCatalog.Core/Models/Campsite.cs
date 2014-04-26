using System;
using System.Collections.Generic;
using TouristRouteCatalog.Core.Model;

namespace TouristRouteCatalog.Core.Models
{
    public partial class Campsite : IModel
    {
        public Campsite()
        {
            this.Routes = new List<Route>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
