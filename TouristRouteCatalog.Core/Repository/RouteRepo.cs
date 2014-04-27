using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using TouristRouteCatalog.Core.DAL;
using TouristRouteCatalog.Core.Proxy.Routes;

namespace TouristRouteCatalog.Core.Repository
{
    public class RouteRepo : BaseRepo<RouteRepo>
    {
        [InjectionConstructor]
        public RouteRepo(TouristCatalogModelEntity context)
            : base(context)
        {
        }

        public List<RouteProxy> GetAllRoutes()
        {
            return Context.Routes.Select(r =>
                new RouteProxy()
                {
                    Id = r.Id,
                    CreatorId = r.CreatorId,
                    Description = r.Description,
                    DifficultyLevel = r.DifficultyLevel,
                    Duration = r.Duration,
                    Name = r.Name,
                    PublicTransport = r.PublicTransport,
                    Seasons = r.Seasons
                }).ToList();
        }

        public RouteProxy GetRouteById(int id)
        {
            var route = Context.Routes.FirstOrDefault(r => r.Id == id);
            if (route != null)
            {
                return new RouteProxy(route);
            }
            return null;
        }

        public bool UpdateRoute(RouteProxy routeProxy)
        {
            var route = Context.Routes.FirstOrDefault(r => r.Id == routeProxy.Id);
            if (route != null)
            {
                route.Name = routeProxy.Name;
                route.DifficultyLevel = routeProxy.DifficultyLevel;
                route.Duration = routeProxy.Duration;
                route.Description = routeProxy.Description;
                route.Seasons = routeProxy.Seasons;
                route.PublicTransport = routeProxy.PublicTransport;
                Context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}