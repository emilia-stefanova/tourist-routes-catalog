using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristRouteCatalog.Core.Model;
using TouristRouteCatalog.Web.Controllers.Abstract;

namespace TouristRouteCatalog.Web.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            var testModel = LoadModel<TestModel>();
            var data = testModel.GetProxy();
            return View(data);
        }

    }
}
