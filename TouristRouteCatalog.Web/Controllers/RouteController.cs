﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristRouteCatalog.Web.Controllers.Abstract;
using TouristRouteCatalog.Core.Model;
using TouristRouteCatalog.Core.Proxy.Routes;

namespace TouristRouteCatalog.Web.Controllers
{
    public class RouteController : BaseController
    {

        public ActionResult Index()
        {
            var rm = LoadModel<RoutesModel>();
            var routes = rm.GetAllRoutes();

            return View(routes);
        }

        public ActionResult Details(int id)
        {
            var rm = LoadModel<RoutesModel>();

            return View(rm.GetRouteById(id));
        }

        public ActionResult Edit(int id)
        {
            var rm = LoadModel<RoutesModel>();

            return View(rm.GetRouteById(id));
        }

        [HttpPost]
        public ActionResult Edit(RouteProxy route)
        {
            if (ModelState.IsValid)
            {
                var rm = LoadModel<RoutesModel>();
                if (rm.UpdateRoute(route))
                {
                    return RedirectToAction("Details", new { id = route.Id });
                }
            }
            return View(route);
        }

        //private static List<RouteProxy> GetDummyRoutes(int count = 50)
        //{
        //    var routes = new List<RouteProxy>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        routes.Add(new RouteProxy()
        //        {
        //            Name = string.Format("Маршрут {0}", i),
        //            Description = string.Format("Описание {0}", i),
        //            Duration = TimeSpan.FromHours(i).Ticks
        //        });
        //    }
        //    return routes;
        //}

    }
}