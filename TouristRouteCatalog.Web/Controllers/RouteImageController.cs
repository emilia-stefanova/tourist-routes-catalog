using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristRouteCatalog.Core.Model;
using TouristRouteCatalog.Core.Proxy;
using TouristRouteCatalog.Web.Controllers.Abstract;

namespace TouristRouteCatalog.Web.Controllers
{
    public class RouteImageController : BaseController
    {
        public ActionResult Add(int id)
        {
            RouteImageProxy rip = new RouteImageProxy();
            rip.RouteId = id;
            return View(rip);
        }

        [HttpPost]
        public ActionResult Add(RouteImageProxy image)
        {
            var rm = LoadModel<RouteImageModel>();
            rm.SaveImages(new List<RouteImageProxy>() { image });

            return RedirectToAction("Details", "Route", new { id = image.RouteId });
        }

        public ActionResult Delete(int id, int routeId)
        {
            if (ModelState.IsValid)
            {
                var rm = LoadModel<RouteImageModel>();
                rm.DeleteRouteImage(id);
            }
            return RedirectToAction("Details", "Route", new { id = routeId });
        }
    }
}
