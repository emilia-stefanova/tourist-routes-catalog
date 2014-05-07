using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;

namespace TouristRouteCatalog.Core.Proxy
{
    public class WaterSourceProxy
    {
        public WaterSourceProxy()
        {

        }

        public WaterSourceProxy(WaterSource waterSource)
        {
            Id = waterSource.Id;
            Description = waterSource.Description;
            Name = waterSource.Name;
            Latitude = waterSource.Latitude.Value;
            Longitude = waterSource.Longitude.Value;
        }

        public int Id { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //public ICollection<RouteProxy> Routes { get; set; }
    }
}
