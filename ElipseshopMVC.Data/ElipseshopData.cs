namespace ElipseshopMVC.Data
{
    using ElipseshopMVC.Common.Repository;
    using ElipseshopMVC.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class ElipseshopData : IElipseshopData
    {
        private readonly DbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public ElipseshopData()
            : this(new ElipseshopDbContext())
        {
        }
        public ElipseshopData(DbContext context)
        {
            this.context = context;
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }
        public IRepository<CategoryLanguage> CategoryLanguages
        {
            get { return this.GetRepository<CategoryLanguage>(); }
        }
        public IRepository<CategoryPicture> CategoryPictures 
        { 
            get { return this.GetRepository<CategoryPicture>(); } 
        }
        public IRepository<Color> Colors
        {
            get { return this.GetRepository<Color>(); }
        }
        public IRepository<ColorLanguage> ColorLanguages
        {
            get { return this.GetRepository<ColorLanguage>(); }
        }
        public IRepository<Language> Languages
        {
            get { return this.GetRepository<Language>(); }
        }
        public IRepository<PCSQuantity> PCSQuantities
        {
            get { return this.GetRepository<PCSQuantity>(); }
        }
        public IRepository<Product> Products
        {
            get { return this.GetRepository<Product>(); }
        }
        public IRepository<ProductLanguage> ProductLanguage
        {
            get { return this.GetRepository<ProductLanguage>(); }
        }
        public IRepository<ProductPicture> ProductPictures
        {
            get { return this.GetRepository<ProductPicture>(); }
        }
        public IRepository<ShoppingBagItem> ShoppingBagItems
        {
            get { return this.GetRepository<ShoppingBagItem>(); }
        }
        public IRepository<Size> Sizes
        {
            get { return this.GetRepository<Size>(); }
        }
        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
