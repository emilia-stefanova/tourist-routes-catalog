using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using TouristRouteCatalog.Core.DAL;
using TouristRouteCatalog.Core.Proxy;

namespace TouristRouteCatalog.Core.Repository
{
    public class CampsiteRepo : BaseRepo<CampsiteProxy>
    {
        [InjectionConstructor]
        public CampsiteRepo(TouristCatalogModelEntity context)
            : base(context)
        {
        }

        public List<CampsiteProxy> GetAllCampsitesForRoute(int routeId)
        {
            return (from campsite in this.Context.Campsites
                    where campsite.Routes.Any(r => r.Id == routeId)
                    select new CampsiteProxy()
                    {
                        Id = campsite.Id,
                        Description = campsite.Description,
                        Latitude = campsite.Latitude,
                        Longitude = campsite.Longitude,
                        Name = campsite.Name,
                        TypeId = campsite.TypeId
                    }).ToList();
        }
    }
}
