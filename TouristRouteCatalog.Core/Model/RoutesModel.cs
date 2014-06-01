using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;
using TouristRouteCatalog.Core.Proxy;
using TouristRouteCatalog.Core.Repository;

namespace TouristRouteCatalog.Core.Model
{
    public class RoutesModel : IModel
    {
        #region Repositories
        [Dependency]
        public RouteRepo RouteRepo { get; set; }

        [Dependency]
        public RouteGeoPointRepo RouteGeoPointRepo { get; set; }

        [Dependency]
        public RouteImageRepo RouteImageRepo { get; set; }

        [Dependency]
        public WaterSourceRepo WaterSourceRepo { get; set; }

        [Dependency]
        public CampsiteRepo CampsiteRepo { get; set; }
        #endregion

        public void Init()
        {

        }

        public List<RouteProxy> GetAllRoutes(string search = null)
        {
            var allRoutes = RouteRepo.GetAllRoutes();
            foreach (var route in allRoutes)
            {
                route.Campsites = CampsiteRepo.GetAllCampsitesForRoute(route.Id);
                route.WaterSources = WaterSourceRepo.GetAllWaterSourcesForRoute(route.Id);
                route.Images = RouteImageRepo.GetAllImagesForRoute(route.Id);
                route.GeoPoints = RouteGeoPointRepo.GetAllRouteGeoPointForRoute(route.Id);
            }

            if (search != null)
	        {
                allRoutes.RemoveAll(item => (item.Name != null && !item.Name.Contains(search))  &&
                    ( item.Description != null && !item.Description.Contains(search)));
	        }
            

            return allRoutes;
        }

        public RouteProxy GetRouteById(int id)
        {
            var route = RouteRepo.GetRouteById(id);
            route.Campsites = CampsiteRepo.GetAllCampsitesForRoute(route.Id);
            route.WaterSources = WaterSourceRepo.GetAllWaterSourcesForRoute(route.Id);
            route.Images = RouteImageRepo.GetAllImagesForRoute(id);
            route.GeoPoints = RouteGeoPointRepo.GetAllRouteGeoPointForRoute(id);
            return route;
        }

        public bool UpdateRoute(RouteProxy routeProxy)
        {
            var route = RouteRepo.GetDbRouteById(routeProxy.Id);
            if (route != null)
            {
                List<RouteGeoPoint> geoPoints = new List<RouteGeoPoint>();
                if (routeProxy.GeoPoints != null)
                {
                    routeProxy.GeoPoints.ToList().ForEach(item =>
                    {
                        geoPoints.Add(new RouteGeoPoint() { Latitude = item.Latitude, Longitude = item.Longitude });
                    });
                }
                route.Name = routeProxy.Name;
                route.DifficultyLevel = routeProxy.DifficultyLevel;
                route.Duration = routeProxy.Duration;
                route.Description = routeProxy.Description;
                route.Seasons = routeProxy.Seasons;
                route.PublicTransport = routeProxy.PublicTransport;


                RouteGeoPointRepo.DeleteWithoutSave(route.RouteGeoPoints);
                route.RouteGeoPoints = geoPoints;
                RouteRepo.SaveChanges();
                return true;
            }
            return false;
            // TODO: Update the collection properties as well
        }

        public bool CreateRoute(RouteProxy route)
        {
            List<RouteGeoPoint> geoPoints = new List<RouteGeoPoint>();
            if (route.GeoPoints != null)
            {
                route.GeoPoints.ToList().ForEach(item =>
                {
                    geoPoints.Add(new RouteGeoPoint() { Latitude = item.Latitude, Longitude = item.Longitude });
                });
            }
            Route newRoute = new Route()
            {
                Name = route.Name,
                DifficultyLevel = route.DifficultyLevel,
                Duration = route.Duration,
                Description = route.Description,
                Seasons = route.Seasons,
                PublicTransport = route.PublicTransport,
                CreatorId = route.CreatorId,
                RouteGeoPoints = geoPoints
            };
            RouteRepo.Add(newRoute);

            RouteRepo.SaveChanges();
            return true;
        }

        public bool DeleteRoute(int routeId)
        {
            return RouteRepo.Delete(RouteRepo.GetDbRouteById(routeId)) == 1;
        }
    }
}
