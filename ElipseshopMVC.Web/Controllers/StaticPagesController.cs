namespace ElipseshopMVC.Web.Controllers
{
    using ElipseshopMVC.Data;
    using System.Web.Mvc;

    public class StaticPagesController : BaseController
    {
        public StaticPagesController(IElipseshopData data)
            : base(data)
        {
        }

        public ActionResult Home()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}