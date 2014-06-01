using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;
using TouristRouteCatalog.Core.Proxy;

namespace TouristRouteCatalog.Core.Repository
{
    public class RouteImageRepo : BaseRepo<RouteImage>
    {
        [InjectionConstructor]
        public RouteImageRepo(TouristCatalogModelEntity context)
            : base(context)
        {
        }

        public List<RouteImageProxy> GetAllImagesForRoute(int routeId)
        {
            return (from image in this.Context.RouteImages
                    where image.RouteId == routeId
                    select new RouteImageProxy()
                    {
                        Id = image.Id,
                        Description = image.Description,
                        Name = image.Name,
                        ImageLocation = image.ImageLocation,
                        Latitude = image.Latitude,
                        Longitude = image.Longitude,
                        RouteId = image.RouteId
                    }).ToList();
        }

        public RouteImageProxy GetImageRouteForId(int imageId)
        {
            return (from image in this.Context.RouteImages
                    where image.Id == imageId
                    select new RouteImageProxy()
                    {
                        Id = image.Id,
                        Description = image.Description,
                        Name = image.Name,
                        ImageLocation = image.ImageLocation,
                        Latitude = image.Latitude,
                        Longitude = image.Longitude,
                        RouteId = image.RouteId
                    }).FirstOrDefault();
        }

        public RouteImage GetDbImageRouteForId(int imageId)
        {
            return (from image in this.Context.RouteImages
                    where image.Id == imageId
                    select image).FirstOrDefault();
        }
    }
}
