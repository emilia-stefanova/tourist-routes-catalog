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
    public class RouteImageModel : IModel
    {
        #region Repositories
        [Dependency]
        public RouteImageRepo routeImageRepo { get; set; }
        #endregion

        public void Init()
        {

        }

        public List<RouteImageProxy> GetAllImagesForRoute(int routeId)
        {
            return routeImageRepo.GetAllImagesForRoute(routeId);
        }

        public RouteImageProxy GetImageRouteForId(int imageId)
        {
            return routeImageRepo.GetImageRouteForId(imageId);
        }

        public void SaveImages(List<RouteImageProxy> routes)
        {
            routes.ForEach(item =>
            {
                routeImageRepo.Add(item);
            });
            routeImageRepo.SaveChanges();
        }
    }
}
