namespace ElipseshopMVC.Web
{
    using System.Web;

    public static class SiteMapNodeExtender
    {
        public static SiteMapNode GetTopCategoryNode(this SiteMapNode node)
        {
            SiteMapNode topCategoryNode = node;
            while (topCategoryNode.ParentNode.Key != "0")
            {
                topCategoryNode = topCategoryNode.ParentNode;
            }

            return topCategoryNode;
        }
    }
}