namespace ElipseshopMVC.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ElipseshopMVC.Common;
    using ElipseshopMVC.Data;
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure;
    using ElipseshopMVC.Web.Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    
    public class ProductController : BaseController
    {
        public ProductController(IElipseshopData data)
            : base(data)
        {
        }

        public ActionResult Product(int productID)
        {
            //Need to add some additional dll
            //Contract.Requires<ArgumentOutOfRangeException>(productID > 0, "Product ID should be greater than 0");

            Languages language = Languages.BG;

            ProductViewModel product = this.Data.Products
                .All()
                .Where(x => x.ID == productID)
                .Project()
                .To<ProductViewModel>(new { language = language })
                .FirstOrDefault();

            if (product == null)
            {
                return new HttpNotFoundResult("Product not found!");
            }

            SetDropDownListsProperties(product);

            SiteMapNode currentNode = SiteMapProvider.FindSiteMapNodeFromKey(product.IDCategory.ToString());
            product.StarttingNode = currentNode.GetTopCategoryNode();

            ViewBag.CurrentNodeKey = currentNode.Key;
            ViewBag.Title = product.Title;

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Product(ProductOrderViewModel productOrder)
        {
            if (this.Session[GlobalConstants.ShoppingBagKey] == null)
            {
                this.Session[GlobalConstants.ShoppingBagKey] = new List<ShoppingBagItemViewModel>();
            }

            IList<ShoppingBagItemViewModel> shoppingBag = this.Session[GlobalConstants.ShoppingBagKey] as List<ShoppingBagItemViewModel>;
            if (shoppingBag == null)
            {
                throw new ArgumentNullException("shoppingBag", "The ShoppingBag property in the Session has invalid type");
            }

            if (shoppingBag != null)
            {
                ShoppingBagItemViewModel shoppingBagItem = new ShoppingBagItemViewModel
                {
                    IDPCSQuantities = productOrder.PCSQuantityID,
                    Quantity = productOrder.ProductQuantity,
                };

                shoppingBag.Add(shoppingBagItem);

                if (this.Request.IsAuthenticated)
                {
                    string userId = this.User.Identity.GetUserId();
                    shoppingBagItem.IDUser = userId;

                    ShoppingBagItem entity = Mapper.Map<ShoppingBagItemViewModel, ShoppingBagItem>(shoppingBagItem);
                    this.Data.ShoppingBagItems.Add(entity);
                    this.Data.SaveChanges();
                }

                TempData["Success"] = "You have successfully added the product in the Shopping bag!";
            }

            return Redirect(this.HttpContext.Request.UrlReferrer.AbsolutePath);
        }

        [AjaxRequest]
        [HttpPost]
        public JsonResult GetSizes(int productID, string colorHexCode)
        {
            Languages language = Languages.BG;

            ProductViewModel product = this.Data.Products
                .All()
                .Where(x => x.ID == productID)
                .Project()
                .To<ProductViewModel>(new { language = language})
                .FirstOrDefault();

            IEnumerable<SelectListItem> sizes = product.PCSQuantities
                .Where(x => x.Color.HexCode == colorHexCode)
                .Select(p => new SelectListItem { Value = p.Size.ID.ToString(), Text = p.Size.Name });

            return Json(new SelectList(sizes, "Value", "Text"));
        }
        [AjaxRequest]
        [HttpPost]
        public JsonResult GetQuantities(int productID, string colorHexCode, int sizeID)
        {
            Languages language = Languages.BG;

            ProductViewModel product = this.Data.Products
                .All()
                .Where(x => x.ID == productID)
                .Project()
                .To<ProductViewModel>(new { language = language })
                .FirstOrDefault();

            PCSQuantitiesViewModel selectedPCSQuantity = product.PCSQuantities
                .Where(x => x.Color.HexCode == colorHexCode && x.Size.ID == sizeID)
                .FirstOrDefault();

            IEnumerable<SelectListItem> quantities = GetProductQuantities(selectedPCSQuantity.Quantity);

            var result = new 
            { 
                Quantities = new SelectList(quantities, "Value", "Text"), 
                PCSQuantityID = selectedPCSQuantity.ID 
            };

            return Json(result);
        }

        private static void SetDropDownListsProperties(ProductViewModel product)
        {
            if (product.PCSQuantities != null && product.PCSQuantities.Count() > 0)
            {
                IEnumerable<IGrouping<ColorViewModel, PCSQuantitiesViewModel>> productColorGroups
                    = product.PCSQuantities.GroupBy(p => p.Color);

                IGrouping<ColorViewModel, PCSQuantitiesViewModel> firstProductColorGroup = productColorGroups.FirstOrDefault();

                PCSQuantitiesViewModel firstPCSQuantity = firstProductColorGroup.First(); ;

                if (productColorGroups.Count() == 1 && firstProductColorGroup.Key == null)
                {
                    product.SelectedPCSQuantityID = firstProductColorGroup.Select(x => x.ID).FirstOrDefault();
                }
                else
                {
                    product.ProductColors = new List<SelectListItem>();
                    int counter = 1;
                    foreach (var group in productColorGroups)
                    {
                        product.ProductColors
                            .Add(new SelectListItem { Value = group.Key.HexCode, Text = group.Key.Name, Selected = counter == 1 });
                        counter++;
                    }

                    product.ProductSizes = firstProductColorGroup
                        .Select(p => new SelectListItem { Value = p.Size.ID.ToString(), Text = p.Size.Name }).ToList();
                    product.ProductSizes.FirstOrDefault().Selected = true;
                }

                product.ProductQuantities = GetProductQuantities(firstPCSQuantity.Quantity);
                product.SelectedPCSQuantityID = firstPCSQuantity.ID;
            }
        }
        private static IList<SelectListItem> GetProductQuantities(int productQuantity)
        {
            List<SelectListItem> productQuantities = new List<SelectListItem>();
            for (int i = 1; i <= productQuantity; i++)
            {
                productQuantities.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString(), Selected = i == 1 });
            }

            return productQuantities;
        }
    }
}