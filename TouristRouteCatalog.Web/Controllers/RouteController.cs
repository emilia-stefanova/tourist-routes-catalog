﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristRouteCatalog.Web.Controllers.Abstract;
using TouristRouteCatalog.Core.Model;
using TouristRouteCatalog.Core.Proxy;

namespace TouristRouteCatalog.Web.Controllers
{
    public class RouteController : BaseController
    {

        public RouteController()
        {
            //var ctx = new TouristRouteCatalog.Core.DAL.TouristCatalogModelEntity();
            //ctx.Users.Add(new Core.DAL.User()
            //    {
            //        Description = "This is the default user.",
            //        Name = "Default User"
            //    });

            //ctx.SaveChanges();
            //ctx.RouteDifficultyLevels.Add(
            //    new Core.DAL.RouteDifficultyLevel()
            //    {
            //        Difficulty = 0,
            //        Name = "Много лесно"
            //    });

            //ctx.RouteDifficultyLevels.Add(
            //    new Core.DAL.RouteDifficultyLevel()
            //    {
            //        Difficulty = 1,
            //        Name = "Лесно"
            //    });

            //ctx.RouteDifficultyLevels.Add(
            //    new Core.DAL.RouteDifficultyLevel()
            //    {
            //        Difficulty = 2,
            //        Name = "Средно"
            //    });

            //ctx.RouteDifficultyLevels.Add(
            //    new Core.DAL.RouteDifficultyLevel()
            //    {
            //        Difficulty = 3,
            //        Name = "Трудно"
            //    });

            //ctx.RouteDifficultyLevels.Add(
            //    new Core.DAL.RouteDifficultyLevel()
            //    {
            //        Difficulty = 4,
            //        Name = "Много Трудно"
            //    });

            //ctx.SaveChanges();
        }

        public ActionResult Index(string search, double? lat, double? lng)
        {
            var rm = LoadModel<RoutesModel>();
            var routes = rm.GetAllRoutes(search, lat, lng);

            return View(routes);
        }

        public ActionResult Details(int id)
        {
            var rm = LoadModel<RoutesModel>();
            var route = rm.GetRouteById(id);
            return View(route);
        }

        public ActionResult Edit(int id)
        {
            var rm = LoadModel<RoutesModel>();

            return View(rm.GetRouteById(id));
        }

        [HttpPost]
        public ActionResult Edit(RouteProxy route)
        {
            route.CreatorId = 1;
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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RouteProxy route)
        {
            route.CreatorId = 1;
            if (ModelState.IsValid)
            {
                var rm = LoadModel<RoutesModel>();
                rm.CreateRoute(route);
                return RedirectToAction("Index");
            }
            return View(route);
        }

        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var rm = LoadModel<RoutesModel>();
                rm.DeleteRoute(id);
            }
            return RedirectToAction("Index");
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
