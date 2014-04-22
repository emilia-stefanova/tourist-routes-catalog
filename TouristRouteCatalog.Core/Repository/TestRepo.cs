using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;
using TouristRouteCatalog.Core.Proxy.Test;

namespace TouristRouteCatalog.Core.Repository
{
    public class TestRepo : BaseRepo<TestProxy>
    {
        #region Constructors

        [InjectionConstructor]
        public TestRepo(TouristCatalogModelEntity context)
            : base(context)
        {
        }

        #endregion

        public List<TestProxy> Test()
        {
            var query = (from c in this.Context.Users
                         select new TestProxy { 
                             username = c.Name
                         }).ToList();
            return query;
        }
    }
}
