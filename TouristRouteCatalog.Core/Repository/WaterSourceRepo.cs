using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using TouristRouteCatalog.Core.DAL;
using TouristRouteCatalog.Core.Proxy;

namespace TouristRouteCatalog.Core.Repository
{
    public class WaterSourceRepo : BaseRepo<WaterSourceProxy>
    {
        [InjectionConstructor]
        public WaterSourceRepo(TouristCatalogModelEntity context)
            : base(context)
        {
        }

        public List<WaterSourceProxy> GetAllWaterSourcesForRoute(int routeId)
        {
            return (from waterSource in this.Context.WaterSources
                    where waterSource.Routes.Any(r => r.Id == routeId)
                    select new WaterSourceProxy()
                    {
                        Id = waterSource.Id,
                        Description = waterSource.Description,
                        Latitude = waterSource.Latitude.Value,
                        Longitude = waterSource.Longitude.Value,
                        Name = waterSource.Name
                    }).ToList();
        }
    }
}
