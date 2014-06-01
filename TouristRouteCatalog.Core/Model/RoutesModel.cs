using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            return route;
        }

        public bool UpdateRoute(RouteProxy route)
        {
            return RouteRepo.UpdateRoute(route);
            // TODO: Update the collection properties as well
        }

        public bool CreateRoute(RouteProxy route)
        {
            RouteRepo.CreateRoute(route);
            return true;
        }

        public bool DeleteRoute(int routeId)
        {
            return RouteRepo.Delete(RouteRepo.GetRouteById(routeId)) == 1;
        }
    }
}
