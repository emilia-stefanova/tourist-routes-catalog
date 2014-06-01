using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;
using TouristRouteCatalog.Core.Proxy;
using TouristRouteCatalog.Core.Proxy.Test;

namespace TouristRouteCatalog.Core.Repository
{
    public class RouteGeoPointRepo : BaseRepo<RouteGeoPoint>
    {
        [InjectionConstructor]
        public RouteGeoPointRepo(TouristCatalogModelEntity context)
            : base(context)
        {
        }


        public List<RouteGeoPointProxy> GetAllRouteGeoPointForRoute(int routeId)
        {
            return (from geoPoint in this.Context.RouteGeoPoints
                    where geoPoint.RouteId == routeId
                    select new RouteGeoPointProxy()
                    {
                        Id = geoPoint.Id,
                        Latitude = geoPoint.Latitude,
                        Longitude = geoPoint.Longitude,
                        OrderIndex = geoPoint.OrderIndex,
                        RouteId = geoPoint.RouteId
                    }).ToList();
        }

    }
}