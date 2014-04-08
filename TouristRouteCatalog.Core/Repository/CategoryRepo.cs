//using Microsoft.Practices.Unity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TouristRouteCatalog.Core.Repository
//{
//    public class CategoryRepo : BaseRepo<ArticleCategory>
//    {

//        #region Constructors

//        [InjectionConstructor]
//        public CategoryRepo(ShopNBCEntities context)
//            : base(context)
//        {
//        }

//        #endregion
//        #region public methods

//        public bool HasAnyChangesAfterDate(DateTime date)
//        {
//            bool hasChanges = (from art in Context.Articles
//                               where art.ModifiedDate > date
//                               select art).Any();
//            if (!hasChanges)
//            {
//                hasChanges = (from artCat in Context.ArticleCategories
//                              where artCat.ModifiedDate > date
//                              select artCat).Any();
//            }
//            return hasChanges;

//        }

//        public List<CategoriesOrderSingle> GetCategoryOrder(int? CategoryID, bool? IncludeTags)
//        {
//            // Do not include categories that have articles without tags.
//            var getCategories = (from cat in Context.ArticleCategories
//                                 where cat.IsDeleted == false && (CategoryID == null || CategoryID == cat.ArticleCategoryID)
//                                 && cat.Articles.Count() > 0 && cat.Articles.Count(a => a.ArticleRecords.Count(r => r.ArticleToArticleTags.Count > 0) > 0) > 0
//                                 orderby cat.OrderIndex
//                                 select cat);

//            if (IncludeTags != null && IncludeTags.Value)
//            {
//                return (from cat in getCategories
//                        select new CategoriesOrderSingle
//                        {
//                            CategoryID = cat.ArticleCategoryID,
//                            CategoryName = cat.Name,
//                            HasTagGrouping = cat.HasTagGrouping,
//                            Tags = (from art in cat.Articles.Where(item => (item.IsDeleted == null || !item.IsDeleted.Value))
//                                    join artRec in Context.ArticleRecords on art.ArticleID equals artRec.ArticleID
//                                    join artTag in Context.ArticleToArticleTags on artRec.ArticleRecordID equals artTag.ArticleRecordID
//                                    join tag in Context.ArticleTags on artTag.ArticleTagID equals tag.ArticleTagID
//                                    where artRec.IsCurrent && ((artRec.StartDate <= DateTime.UtcNow && (artRec.EndDate == null || DateTime.UtcNow <= artRec.EndDate)) || (artRec.Featured && artRec.StartDateFeatured != null && artRec.EndDateFeatured != null && artRec.StartDateFeatured <= DateTime.UtcNow && DateTime.UtcNow <= artRec.EndDateFeatured))
//                                    select new ArticleTagProxy
//                                    {
//                                        Tag = artTag.ArticleTag.Name,
//                                        AdditionalInfo = (from artToArtTag in Context.ArticleToArticleTags
//                                                          join artRecIn in Context.ArticleRecords.Where(item =>

//                                                              (item.Article.IsDeleted == null || !item.Article.IsDeleted.Value) &&
//                                                              item.IsCurrent &&
//                                                              ((item.StartDate <= DateTime.UtcNow && (item.EndDate == null || DateTime.UtcNow <= item.EndDate)) ||
//                                                              (item.Featured && item.StartDateFeatured != null && item.EndDateFeatured != null && item.StartDateFeatured <= DateTime.UtcNow && DateTime.UtcNow <= item.EndDateFeatured))) on artToArtTag.ArticleRecordID equals artRecIn.ArticleRecordID
//                                                          where artToArtTag.ArticleTagID == artTag.ArticleTagID && artRecIn.Article.ArticleCategoryID != null && artRecIn.Article.ArticleCategoryID == cat.ArticleCategoryID
//                                                          orderby artRecIn.Article.OrderIndex ascending, artRecIn.Article.ArticleCategory.OrderIndex ascending
//                                                          select new ArticleTagAdditionalInfo
//                                                          {
//                                                              ImageURL = artRecIn.ArticleResources.Where(res => res.IsPrimary).FirstOrDefault().ThumbURL,
//                                                              OrderIndex = artRecIn.Article.OrderIndex,
//                                                              HasVideo = artRecIn.ArticleResources.Count(v => (v.IsPrimary == true && (v.IsDeleted == null || v.IsDeleted == false) && (v.ArticleResourceTypeID == (int)ShopNBC.Core.Proxy.Enums.ResourceType.Video || v.ArticleResourceTypeID == (int)ShopNBC.Core.Proxy.Enums.ResourceType.YouTube))) > 0 ? true : false
//                                                          }).FirstOrDefault()
//                                    }).Distinct()
//                        }).ToList();
//            }
//            else
//            {
//                return (from cat in getCategories
//                        select new CategoriesOrderSingle
//                        {
//                            CategoryID = cat.ArticleCategoryID,
//                            CategoryName = cat.Name,
//                            HasTagGrouping = cat.HasTagGrouping
//                        }).ToList();
//            }
//        }

//        public List<CategoriesProxy> GetAllCategories()
//        {
//            List<CategoriesProxy> AllCategories = (from cat in Context.ArticleCategories
//                                                   where cat.IsDeleted == false
//                                                   select new CategoriesProxy
//                                                   {
//                                                       CategoryID = cat.ArticleCategoryID,
//                                                       CategoryName = cat.Name,
//                                                       CategoryOrderIndex = cat.OrderIndex,
//                                                       HasTagGrouping = cat.HasTagGrouping,
//                                                       CanEdit = cat.CanEdit
//                                                   }).ToList();
//            return AllCategories;
//        }

//        public CategoriesProxy GetCategoryByID(int? id)
//        {
//            CategoriesProxy CategoryByID = (from cat in Context.ArticleCategories
//                                            where cat.ArticleCategoryID == id
//                                            select new CategoriesProxy
//                                            {
//                                                CategoryID = cat.ArticleCategoryID,
//                                                CategoryName = cat.Name,
//                                                CategoryOrderIndex = cat.OrderIndex,
//                                                HasTagGrouping = cat.HasTagGrouping,
//                                                CanEdit = cat.CanEdit
//                                            }).FirstOrDefault();
//            return CategoryByID;
//        }

//        public List<CategoriesProxy> GetCategoriesByOrderIndex()
//        {
//            List<CategoriesProxy> CategoriesByOrder = (from cat in Context.ArticleCategories
//                                                       orderby cat.OrderIndex
//                                                       where cat.IsDeleted == false
//                                                       select new CategoriesProxy
//                                                       {
//                                                           CategoryID = cat.ArticleCategoryID,
//                                                           CategoryName = cat.Name,
//                                                           CategoryOrderIndex = cat.OrderIndex,
//                                                           HasTagGrouping = cat.HasTagGrouping,
//                                                           CanEdit = cat.CanEdit
//                                                       }).ToList();
//            return CategoriesByOrder;
//        }

//        public ArticleCategory GetArticleCategoryById(int id)
//        {
//            return Context.ArticleCategories.Where(cat => cat.ArticleCategoryID == id).FirstOrDefault();
//        }

//        public ArticleCategory GetArticleCategoryByName(string Name)
//        {
//            return Context.ArticleCategories.Where(cat => cat.Name == Name && cat.IsDeleted == null).FirstOrDefault();
//        }

//        public List<Article> GetAllArticlesByCategoryID(int id)
//        {
//            return Context.Articles.Where(cat => cat.ArticleCategoryID == id).ToList();
//        }

//        public List<ArticleCategory> GetCategoriesByIDs(List<int> ids)
//        {
//            return (from art in Context.ArticleCategories
//                    join cids in ids on art.ArticleCategoryID equals cids
//                    select art).ToList();
//        }
//        #endregion
//    }
//}
