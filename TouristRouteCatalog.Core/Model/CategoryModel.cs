//using Microsoft.Practices.Unity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TouristRouteCatalog.Core.Model;

//namespace TouristRouteCatalog.Core.Model
//{
//    public class CategoryModel : IModel, IDisposable
//    {
//        #region Repositories
//        [Dependency]
//        public CategoryRepo CategoryRep { get; set; }
//        #endregion

//        public void Init()
//        {

//        }

       
//        public List<CategoriesProxy> GetAllCategories()
//        {
//            return CategoryRep.GetAllCategories();
//        }
//        public CategoriesProxy GetCategoryByID(int? id)
//        {
//            return CategoryRep.GetCategoryByID(id);
//        }

//        public List<CategoriesProxy> GetCategoriesByOrderIndex()
//        {
//            return CategoryRep.GetCategoriesByOrderIndex();
//        }

//        public ArticleCategory GetArticleCategoryByID(int id)
//        {
//            return CategoryRep.GetArticleCategoryById(id);
//        }

//        public ArticleCategory GetArticleCategoryByName(string Name)
//        {
//            return CategoryRep.GetArticleCategoryByName(Name);
//        }

//        public bool ValidateCategoryNameExisisting(string Name, int? id)
//        {
//            ArticleCategory cat = GetArticleCategoryByName(Name);
//            return cat == null || cat.ArticleCategoryID == id;
//        }

//        public long InsertCategory(ArticleCategory category)
//        {
//            return CategoryRep.Create(category);
//        }

//        public int UpdateCategory(ArticleCategory category)
//        {
//            category.ModifiedDate = DateTime.UtcNow;
//            CategoryRep.Attach(category);
//            return CategoryRep.SaveChanges();
//        }

//        public int UpdateCategory(List<ArticleCategory> categories)
//        {
//            categories.ForEach(item => item.ModifiedDate = DateTime.UtcNow);
//            CategoryRep.Attach(categories);
//            return CategoryRep.SaveChanges();
//        }

//        public void SaveCategory(CategoriesProxy category)
//        {
//            if (category.CategoryID != null)
//            {
//                ArticleCategory arcat = this.GetArticleCategoryByID(category.CategoryID.Value);
//                if (arcat == null)
//                {
//                    throw new Exception("error");
//                }
//                arcat.Name = category.CategoryName;
//                arcat.ModifiedDate = DateTime.UtcNow;
//                if (arcat.CanEdit)
//                {
//                    arcat.HasTagGrouping = category.HasTagGrouping;
//                }
//                CategoryRep.SaveChanges();
//            }
//            else
//            {
//                ArticleCategory cat = new ArticleCategory()
//                {
//                    Name = category.CategoryName,
//                    OrderIndex = 5000,
//                    IsDeleted = false,
//                    HasTagGrouping = category.HasTagGrouping,
//                    CanEdit = category.CanEdit,
//                    ModifiedDate = DateTime.UtcNow
//                };
//                this.InsertCategory(cat);
//            }
//        }

//        public int DeleteCategory(int id)
//        {
//            ArticleCategory arcat = this.GetArticleCategoryByID(id);
//            List<Article> articles = CategoryRep.GetAllArticlesByCategoryID(id);
//            articles.ForEach(i => i.ArticleCategory = null);
//            if (arcat != null)
//            {
//                arcat.ModifiedDate = DateTime.UtcNow;
//                arcat.IsDeleted = true;
//                arcat.DeletedDate = DateTime.UtcNow;
//            }

//            return CategoryRep.SaveChanges();

//        }

//        public void ReorderCategories(int[] categoryIds)
//        {
//            if (categoryIds != null)
//            {
//                List<int> cids = categoryIds.ToList();
//                List<ArticleCategory> categories = CategoryRep.GetCategoriesByIDs(cids);
//                int j = 0;
//                cids.ForEach(item =>
//                {
//                    ArticleCategory cat = categories.FirstOrDefault(cats => cats.ArticleCategoryID == item);
//                    if (cat != null)
//                    {
//                        cat.OrderIndex = j;
//                    }
//                    cat.ModifiedDate = DateTime.UtcNow;
//                    j++;
//                });
//                CategoryRep.SaveChanges();
//            }
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                // free managed resources
//                if (this.CategoryRep != null)
//                {
//                    this.CategoryRep.Dispose();
//                    this.CategoryRep = null;
//                }
//            }

//            // free native resources if there are any.

//        }

//    }
//}
