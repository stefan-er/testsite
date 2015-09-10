namespace ElipseshopMVC.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using ElipseshopMVC.Common;
    using ElipseshopMVC.Data;
    using ElipseshopMVC.Web.Infrastructure;
    using ElipseshopMVC.Web.Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ShoppingBagController : BaseController
    {
        public ShoppingBagController(IElipseshopData data)
            : base(data)
        {
        }

        public ActionResult Overview()
        {
            Languages language = Languages.BG;

            IEnumerable<ShoppingBagItemViewModel> shoppingBag = new List<ShoppingBagItemViewModel>();

            if (this.Request.IsAuthenticated)
            {
                string userId = this.User.Identity.GetUserId();

                shoppingBag = this.Data.ShoppingBagItems
                    .All()
                    .Where(x => x.IDUser == userId)
                    .Project()
                    .To<ShoppingBagItemViewModel>(new { language = language })
                    .ToList();
            }
            else if (this.Session[GlobalConstants.ShoppingBagKey] != null)
            {
                IEnumerable<ShoppingBagItemViewModel> sessionShoppingBag = this.Session[GlobalConstants.ShoppingBagKey] as IEnumerable<ShoppingBagItemViewModel>;

                if (sessionShoppingBag != null)
                {
                    shoppingBag = sessionShoppingBag;                    
                }
            }

            return View(shoppingBag);
        }
    }
}