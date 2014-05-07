using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;

namespace TouristRouteCatalog.Core.Proxy
{
    public class RouteGeoPointProxy
    {
        public RouteGeoPointProxy()
        {

        }

        public RouteGeoPointProxy(RouteGeoPoint routeGeoPoint)
        {
            Id = routeGeoPoint.Id;
            RouteId = routeGeoPoint.RouteId;
            Latitude = routeGeoPoint.Latitude;
            Longitude = routeGeoPoint.Longitude;
            OrderIndex = routeGeoPoint.OrderIndex;
        }

        public int Id { get; set; }

        internal int RouteId { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int OrderIndex { get; set; }
    }
}
