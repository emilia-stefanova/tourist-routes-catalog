using System;
using System.Collections.Generic;
using TouristRouteCatalog.Core.Model;

namespace TouristRouteCatalog.Core.Models
{
    public partial class User : IModel
    {
        public User()
        {
            this.Routes = new List<Route>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageLocation { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
