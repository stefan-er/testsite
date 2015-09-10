namespace ElipseshopMVC.Web.Controllers
{
    using ElipseshopMVC.Data;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        public BaseController(IElipseshopData data)
        {
            this.Data = data;

            this.SiteMapProvider = new SqlSiteMapProvider();

            //string defaultSiteMapProvider = System.Configuration.ConfigurationManager.AppSettings["defaultSiteMapProvider"];
            //string siteMapCacheTime = System.Configuration.ConfigurationManager.AppSettings["siteMapCacheTime"];

            //System.Collections.Specialized.NameValueCollection attributes = new System.Collections.Specialized.NameValueCollection();
            //attributes.Add("cacheTime", siteMapCacheTime);

            //this.SiteMapProvider.Initialize(defaultSiteMapProvider, attributes);
        }

        protected IElipseshopData Data { get; private set; }
        protected SqlSiteMapProvider SiteMapProvider { get; private set; }
    }
}