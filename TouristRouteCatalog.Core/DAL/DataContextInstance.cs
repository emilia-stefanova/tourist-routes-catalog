using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;

namespace ShopNBC.Core.DAL
{
    public class DataContextInstance
    {
        private static TouristCatalogModelEntity _TouristCatalogModelEntityContext;
        public static TouristCatalogModelEntity TouristCatalogModelEntityContext
        {
            get
            {
                if (_TouristCatalogModelEntityContext == null)
                {
                    _TouristCatalogModelEntityContext = new TouristCatalogModelEntity();
                }
                return _TouristCatalogModelEntityContext;
                
            }
        }

        private DataContextInstance()
        {
        }
    }
}
