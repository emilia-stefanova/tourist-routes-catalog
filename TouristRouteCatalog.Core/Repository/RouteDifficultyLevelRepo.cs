﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;
using TouristRouteCatalog.Core.Proxy.Test;

namespace TouristRouteCatalog.Core.Repository
{
    public class RouteDifficultyLevelRepo : BaseRepo<RouteDifficultyLevel>
    {
        [InjectionConstructor]
        public RouteDifficultyLevelRepo(TouristCatalogModelEntity context)
            : base(context)
        {
        }
    }
}