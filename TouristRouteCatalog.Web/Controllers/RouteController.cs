using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristRouteCatalog.Web.Controllers.Abstract;
using TouristRouteCatalog.Core.Models;

namespace TouristRouteCatalog.Web.Controllers
{
    public class RouteController : BaseController
    {

        //
        // GET: /TouristRoute/

        public ActionResult Index()
        {
            return View();
        }

    }
}
