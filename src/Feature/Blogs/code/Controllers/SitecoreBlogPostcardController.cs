using BlogWebsiteHelix.Feaure.Blogs.Models;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWebsiteHelix.Feaure.Blogs.Controllers
{
    public class SitecoreBlogPostcardController : Controller
    {
        // GET: SitecoreBlogPostcard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetPostcard()
        {

            var result = new List<ArticleModel>();
            try
            {
                Sitecore.Data.Database database = Sitecore.Configuration.Factory.GetDatabase("web");
                Sitecore.Data.Items.Item[] allItems = database.SelectItems(@"fast:/sitecore/content/BlogWebsite/Articles//*[@@templateid='{DF55902C-D8C5-4167-AB23-C53F1EC5DC40}']");

                foreach (var item in allItems)
                {
                    Sitecore.Data.Fields.ReferenceField dl = item.Fields["Category"];
                    string str = dl != null && dl.TargetItem != null ? dl.TargetItem.Name : "";
                    result.Add(new ArticleModel
                    {
                        Id = FieldRenderer.Render(item, "Id"),
                        Title = FieldRenderer.Render(item, "Title"),
                        ShortDescription = FieldRenderer.Render(item, "ShortDescription"),
                        LongDescription = FieldRenderer.Render(item, "LongDescription"),
                        ArticleSmallImage = FieldRenderer.Render(item, "ArticleSmallImage"),
                        PostedDate = Convert.ToDateTime(FieldRenderer.Render(item, "PostedDate")),
                        Author = FieldRenderer.Render(item, "Author"),
                        Category = str//FieldRenderer.Render(item, "Category"),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = result };
            //var items = Sitecore.Context.Database.SelectItems("{56741DF6-7290-4E29-B0D1-11A05EE63AFF}");

            return View("~/Views/BlogWebsiteHelix/Postcard.cshtml", result);
        }

        public ActionResult GetBlogDetail(string blogId)
        {
            ViewBag.blogId = blogId;
            var result = new ArticleModel();
            try
            {

                var item = Sitecore.Context.Database.GetItem(blogId);
                Sitecore.Data.Fields.ReferenceField dl = item.Fields["Category"];
                string str = dl != null && dl.TargetItem != null ? dl.TargetItem.Name : "";
                List<string> tags = new List<string>();
                Sitecore.Data.Fields.MultilistField multilistField = item.Fields["Tags"];
                if (multilistField != null)
                {
                    foreach (Sitecore.Data.Items.Item tag in multilistField.GetItems())
                    {
                        tags.Add(tag.Name);
                    }
                }
                result = new ArticleModel
                {
                    Id = FieldRenderer.Render(item, "Id"),
                    Title = FieldRenderer.Render(item, "Title"),
                    ItemName = FieldRenderer.Render(item, "ItemName"),
                    LongDescription = FieldRenderer.Render(item, "LargeDescription"),
                    ArticleLargeImage = FieldRenderer.Render(item, "ArticleLargeImage"),
                    PostedDate = Convert.ToDateTime(FieldRenderer.Render(item, "PostedDate")),
                    Author = FieldRenderer.Render(item, "Author"),
                    Category = str,//FieldRenderer.Render(item, "Category"),
                    Tags = tags
                };
            }
            catch (Exception ex)
            {
                throw;
            }
            return View("~/Views/BlogWebsiteHelix/blog-detail.cshtml", result);
        }
    }
}