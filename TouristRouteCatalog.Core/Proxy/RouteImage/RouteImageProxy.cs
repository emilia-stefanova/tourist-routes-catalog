using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristRouteCatalog.Core.Proxy.RouteImage
{
    public class RouteImageProxy
    {
        public int Id { get; set; }
        public string ImageLocation { get; set; }
        public int RouteId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
