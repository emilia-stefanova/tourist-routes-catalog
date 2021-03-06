﻿using Microsoft.Practices.Unity;
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

        public List<RouteProxy> GetAllRoutes(string search = null, double? lat = null, double? lng = null)
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
                allRoutes.RemoveAll(item => ((item.Name != null && !item.Name.Contains(search)) || item.Name == null) &&
                    ((item.Description != null && !item.Description.Contains(search)) || item.Description == null));
            }

            if (lat != null && lng != null)
            {
                allRoutes.RemoveAll(item => item.GeoPoints.FirstOrDefault(point => Math.Abs(point.Latitude - lat.Value) < 1 && Math.Abs(point.Longitude - lng.Value) < 1) == null);
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
            var route = RouteRepo.GetDbRouteById(routeId);

            // we'll be better off without saving the routeId for geo points,
            // but as a temp fix this will do
            while (route.RouteGeoPoints.Count > 0)
            {
                RouteGeoPointRepo.Delete(route.RouteGeoPoints.First());
            }
            return RouteRepo.Delete(route) == 1;
        }
    }
}
