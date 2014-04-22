using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.Proxy.Test;
using TouristRouteCatalog.Core.Repository;

namespace TouristRouteCatalog.Core.Model
{
    public class TestModel : IModel
    {
        #region Repositories
        [Dependency]
        public TestRepo TestRep { get; set; }
        #endregion

        public void Init()
        {

        }

        public List<TestProxy> GetProxy()
        {
            return TestRep.Test();
        }
    }
}
