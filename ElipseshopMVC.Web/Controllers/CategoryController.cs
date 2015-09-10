namespace ElipseshopMVC.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using ElipseshopMVC.Data;
    using ElipseshopMVC.Web.Infrastructure;
    using ElipseshopMVC.Web.Models;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CategoryController : BaseController
    {
        public CategoryController(IElipseshopData data)
            : base(data)
        {
        }

        public ActionResult TopCategory(string topCategory)
        {
            Languages language = Languages.BG;

            SiteMapNode currentNode = SiteMapProvider.FindSiteMapNode(Request.RawUrl);

            if (currentNode == null)
            {
                return new HttpNotFoundResult("Category not found!");
            }

            int categoryID = int.Parse(currentNode.Key);

            TopCategoryViewModel topCategoryViewModel = this.Data.Categories
                .All()
                .Where(c => c.ID == categoryID)
                .Project()
                .To<TopCategoryViewModel>(new { language = language })
                .FirstOrDefault();

            topCategoryViewModel.StarttingNode = currentNode;

            ViewBag.CurrentNodeKey = currentNode.Key;

            return View(topCategoryViewModel);
        }
        public ActionResult SubCategory(string topCategory, string subCategory)
        {
            Languages language = Languages.BG;

            SiteMapNode currentNode = SiteMapProvider.FindSiteMapNode(Request.RawUrl);

            if (currentNode == null)
            {
                return new HttpNotFoundResult("Category not found!");
            }

            int categoryID = int.Parse(currentNode.Key);

            SubCategoryViewModel subCategoryViewModel = this.Data.Categories
                .All()
                .Where(c => c.ID == categoryID)
                .Project()
                .To<SubCategoryViewModel>(new { language = language })
                .FirstOrDefault();

            subCategoryViewModel.StarttingNode = currentNode.GetTopCategoryNode();

            ViewBag.CurrentNodeKey = currentNode.Key;

            return View(subCategoryViewModel);
        }     
    }
}