namespace ElipseshopMVC.Web
{
    using System.Web.Mvc;

    public class ViewEnginesConfiguration
    {
        public static void RegisterViewEngines(ViewEngineCollection viewEngineCollection)
        {
            viewEngineCollection.Clear();
            viewEngineCollection.Add(new RazorViewEngine());
        }
    }
}