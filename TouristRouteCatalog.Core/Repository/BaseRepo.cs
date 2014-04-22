using Microsoft.Practices.Unity;
using ShopNBC.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;

namespace TouristRouteCatalog.Core.Repository
{
    public abstract class BaseRepo<TEntity> where TEntity : class
    {
        #region Constructor

        [InjectionConstructor]
        public BaseRepo(TouristCatalogModelEntity context)
        {
            this.Context = context;
        }

        #endregion

        #region Private Variables

        private DbSet<TEntity> objectSet;

        private DbSet<TEntity> ObjectSet
        {
            get
            {
                if (this.objectSet == null)
                {
                    this.objectSet = this.Context.Set<TEntity>();
                }

                return this.objectSet;
            }
        }

        #endregion

        #region Protected Properties

        protected TouristCatalogModelEntity Context { get; set; }

        #endregion

        #region Public Methods

        public virtual void Add(TEntity entity)
        {
            this.ObjectSet.Add(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.Add(entity);
            }
        }

        public virtual void Attach(TEntity entity)
        {
            this.ObjectSet.Attach(entity);
        }

        public virtual void Attach(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.Attach(entity);
            }
        }

        public virtual long Create(TEntity entity)
        {
            this.ObjectSet.Add(entity);

            return this.SaveChanges();
        }

        public virtual void Create(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.Add(entity);
            }

            this.SaveChanges();
        }

        public virtual long Delete(TEntity entity)
        {
            this.ObjectSet.Remove(entity);

            return this.SaveChanges();
        }

        public virtual long Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.Remove(entity);
            }

            return this.SaveChanges();
        }

        public virtual void DeleteWithoutSave(TEntity entity)
        {
            this.ObjectSet.Remove(entity);
        }

        public virtual void DeleteWithoutSave(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.ObjectSet.Remove(entity);
            }
        }

        public virtual int SaveChanges()
        {
            try
            {
                return this.Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                string exception = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    exception = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(exception);
            }
        }

        #endregion

    }

}
