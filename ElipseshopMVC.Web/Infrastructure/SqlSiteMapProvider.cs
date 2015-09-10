namespace ElipseshopMVC.Web
{
    using ElipseshopMVC.Data;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web;
    using System.Linq;
    using System;
    using ElipseshopMVC.Web.Models;
    using System.Web.Caching;

    public class SqlSiteMapProvider : StaticSiteMapProvider
    {
        private SiteMapNode rootNode;
        private bool initialized = false;
        private int cacheTime;

        public override SiteMapNode RootNode
        {
            get
            {
                return BuildSiteMap();
            }
        }
        public virtual bool IsInitialized
        {
            get
            {
                return initialized;
            }
        }

        public override void Initialize(string name, NameValueCollection attributes)
        {
            if (!IsInitialized)
            {
                base.Initialize(name, attributes);
                cacheTime = int.Parse(attributes["cacheTime"]);
            }
        }
        public override SiteMapNode BuildSiteMap()
        {
            lock (this)
            {
                rootNode = HttpContext.Current.Cache["RootNode"] as SiteMapNode;

                if (rootNode == null)
                {
                    Clear();

                    IElipseshopData data = new ElipseshopData();
                    List<SiteMapCategoryViewModel> allCategories = data.Categories.All()
                        .Select(c => new SiteMapCategoryViewModel 
                        { 
                            ID = c.ID,
                            IDParent = c.IDParentCategory,
                            Name = c.CategoryLanguages.FirstOrDefault(cl => cl.IDLanguage == 1).Name
                        }).ToList();

                    Dictionary<int, List<SiteMapCategoryViewModel>> categories = new Dictionary<int, List<SiteMapCategoryViewModel>>();
                    foreach (var category in allCategories)
                    {
                        int parentID = category.IDParent ?? 0;
                        if (!categories.ContainsKey(parentID))
                        {
                            categories[parentID] = new List<SiteMapCategoryViewModel>();
                        }
                        categories[parentID].Add(category);
                    }

                    var root = new SiteMapCategoryViewModel();
                    root.Name = "Home";

                    rootNode = new SiteMapNode(this, "0", "/", root.Name, root.Name);

                    AddNode(rootNode);
                    AddTopCategories(rootNode, categories);

                    HttpContext.Current.Cache.Add("RootNode", rootNode, null, DateTime.Now.AddSeconds(600), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    //HttpContext.Current.Cache.Insert("RootNode", rootNode, null, DateTime.Now.AddSeconds(cacheTime), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }

            return rootNode;
        }
        public override SiteMapNode FindSiteMapNode(string url)
        {
            SiteMapNode rootNode = BuildSiteMap();

            SiteMapNode foundNode = FindNodeRecursively(url, rootNode);

            return foundNode;
        }
        public override SiteMapNode FindSiteMapNodeFromKey(string key)
        {
            SiteMapNode rootNode = BuildSiteMap();

            SiteMapNode foundNode = FindNodeFromKeyRecursively(key, rootNode);

            return foundNode;
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            return BuildSiteMap();
        }
        protected override void Clear()
        {
            lock (this)
            {
                HttpContext.Current.Cache.Remove("RootNode");
                base.Clear();
            }
        } 

        private void AddTopCategories(SiteMapNode rootNode, Dictionary<int, List<SiteMapCategoryViewModel>> categories)
        {
            List<SiteMapCategoryViewModel> topCategories = categories[0];
            foreach (var topCategory in topCategories)
            {
                string url = string.Format("/Category/{0}", topCategory.Name);
                SiteMapNode childNode = new SiteMapNode(this, topCategory.ID.ToString(), url, topCategory.Name, topCategory.Name);
                AddNode(childNode, rootNode);
                AddChildrenCategories(childNode, topCategory.Name, topCategory.ID, categories);
            }
        }
        private void AddChildrenCategories(SiteMapNode rootNode, string topCategoryTitle, int parentID, 
            Dictionary<int, List<SiteMapCategoryViewModel>> categories)
        {
            if (categories.ContainsKey(parentID))
            {
                List<SiteMapCategoryViewModel> childCategories = categories[parentID];
                foreach (var childCategory in childCategories)
                {
                    string url = string.Format("/Category/{0}/{1}", topCategoryTitle, childCategory.Name);
                    SiteMapNode childNode = new SiteMapNode(this, childCategory.ID.ToString(), url, childCategory.Name, childCategory.Name);
                    AddNode(childNode, rootNode);
                    AddChildrenCategories(childNode, topCategoryTitle, childCategory.ID, categories);
                }
            }
        }
        private SiteMapNode FindNodeRecursively(string url, SiteMapNode parrentNode)
        {
            if (parrentNode.Url == url)
            {
                return parrentNode;
            }

            foreach (SiteMapNode childNode in parrentNode.ChildNodes)
            {
                SiteMapNode foundNode = FindNodeRecursively(url, childNode);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }

            return null;
        }
        private SiteMapNode FindNodeFromKeyRecursively(string key, SiteMapNode parrentNode)
        {
            if (parrentNode.Key == key)
            {
                return parrentNode;
            }

            foreach (SiteMapNode childNode in parrentNode.ChildNodes)
            {
                SiteMapNode foundNode = FindNodeFromKeyRecursively(key, childNode);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }

            return null;
        }
    }
}