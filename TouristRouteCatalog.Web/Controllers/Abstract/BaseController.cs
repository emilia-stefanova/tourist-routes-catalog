using ShopNBC.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristRouteCatalog.Core.DAL;
using TouristRouteCatalog.Core.Model;
using TouristRouteCatalog.Services.Helpers;
using WebMatrix.WebData;

namespace TouristRouteCatalog.Web.Controllers.Abstract
{
    public class BaseController : Controller
    {
        public bool IsLogged
        {
            get
            {
                return WebSecurity.CurrentUserName != "";
            }
        }

        private TouristCatalogModelEntity defaultContext;
        private TouristCatalogModelEntity DefaultContext
        {
            get
            {
                if (defaultContext == null)
                {
                    defaultContext = new TouristCatalogModelEntity();
                }
                return defaultContext;
            }
        }

        public T LoadModel<T>(TouristCatalogModelEntity Context = null) where T : IModel
        {
            if (Context == null)
            {
                return ModelHelper.LoadModel<T>(DefaultContext);
            }
            else
            {
                return ModelHelper.LoadModel<T>(Context);
            }
        }



        protected T BuildUp<T>(T obj, TouristCatalogModelEntity Context = null) where T : IModel
        {
            if (Context == null)
            {
                return ModelHelper.BuildUp<T>(obj, DefaultContext);
            }
            else
            {
                return ModelHelper.BuildUp<T>(obj, Context);
            }
        }
    }
}