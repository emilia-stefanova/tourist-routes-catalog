using System;
using System.Collections.Generic;
using TouristRouteCatalog.Core.Model;

namespace TouristRouteCatalog.Core.Models
{
    public partial class RouteDifficultyLevel : IModel
    {
        public RouteDifficultyLevel()
        {
            this.Routes = new List<Route>();
        }

        public int Difficulty { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
