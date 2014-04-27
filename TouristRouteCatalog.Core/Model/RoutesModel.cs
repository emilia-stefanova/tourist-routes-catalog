using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.Proxy.Routes;
using TouristRouteCatalog.Core.Repository;

namespace TouristRouteCatalog.Core.Model
{
    public class RoutesModel : IModel
    {
        #region Repositories
        [Dependency]
        public RouteRepo RouteRepo { get; set; }
        #endregion

        public void Init()
        {

        }

        public List<RouteProxy> GetAllRoutes()
        {
            return RouteRepo.GetAllRoutes();
        }

        public RouteProxy GetRouteById(int id)
        {
            return RouteRepo.GetRouteById(id);
        }

        public bool UpdateRoute(RouteProxy route)
        {
            return RouteRepo.UpdateRoute(route);
        }

        public bool CreateRoute(RouteProxy route)
        {
            RouteRepo.CreateRoute(route);
            return true;
        }
    }
}
