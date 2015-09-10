namespace ElipseshopMVC.Data
{
    using ElipseshopMVC.Common.Repository;
    using ElipseshopMVC.Models;

    public interface IElipseshopData
    {
        IRepository<Category> Categories { get; }
        IRepository<CategoryLanguage> CategoryLanguages { get; }
        IRepository<CategoryPicture> CategoryPictures { get; }
        IRepository<Color> Colors { get; }
        IRepository<ColorLanguage> ColorLanguages { get; }
        IRepository<Language> Languages { get; }
        IRepository<PCSQuantity> PCSQuantities { get; }
        IRepository<Product> Products { get; }
        IRepository<ProductLanguage> ProductLanguage { get; }
        IRepository<ProductPicture> ProductPictures { get; }
        IRepository<ShoppingBagItem> ShoppingBagItems { get; }
        IRepository<Size> Sizes { get; }
        IRepository<User> Users { get; }

        int SaveChanges();
        void Dispose();
    }
}
