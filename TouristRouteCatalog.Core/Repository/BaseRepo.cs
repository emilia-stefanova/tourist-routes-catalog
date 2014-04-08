using Microsoft.Practices.Unity;
using ShopNBC.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;

namespace TouristRouteCatalog.Core.Repository
{
    public abstract class BaseRepo<T> : IDisposable where T : class
    {
        #region Private Variables

        private IObjectSet<T> objectSet;

        private IObjectSet<T> ObjectSet
        {
            get
            {
                if (this.objectSet == null)
                {
                    this.objectSet = this.Context.CreateObjectSet<T>();
                }

                return this.objectSet;
            }
        }

        #endregion

        #region Protected Properties

        protected TouristCatalogModelEntity Context { get; set; }

        #endregion

        #region Constructor

        [InjectionConstructor]
        public BaseRepo(TouristCatalogModelEntity context)
        {
            Context = context;
        }

        #endregion


        #region Public Methods

        public virtual void Add(T entity)
        {
            this.ObjectSet.AddObject(entity);
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.AddObject(entity);
            }
        }

        public virtual void Attach(T entity)
        {
            this.ObjectSet.Attach(entity);
        }

        public virtual void Attach(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.Attach(entity);
            }
        }

        public virtual long Create(T entity)
        {
            this.ObjectSet.AddObject(entity);

            return this.SaveChanges();
        }

        public virtual void Create(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.AddObject(entity);
            }

            this.SaveChanges();
        }

        public virtual long Delete(T entity)
        {
            this.ObjectSet.DeleteObject(entity);

            return this.SaveChanges();
        }

        public virtual long Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.DeleteObject(entity);
            }

            return this.SaveChanges();
        }

        public virtual void DeleteWithoutSave(T entity)
        {
            this.ObjectSet.DeleteObject(entity);
        }

        public virtual void DeleteWithoutSave(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.DeleteObject(entity);
            }
        }

        public virtual void DeleteWithoutSave<G>(IEnumerable<G> entities) where G : class
        {
            IObjectSet<G> ObjectSetLocal = this.Context.CreateObjectSet<G>();

            for (int i = 0; i < entities.Count(); i++)
            {
                ObjectSetLocal.DeleteObject(entities.ElementAt(i));
            }
        }
        public virtual int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        #endregion



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }

            // free native resources if there are any.

        }
    }
}
