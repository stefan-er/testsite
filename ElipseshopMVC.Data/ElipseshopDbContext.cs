namespace ElipseshopMVC.Data
{
    using ElipseshopMVC.Common.Models;
    using ElipseshopMVC.Data.Migrations;
    using ElipseshopMVC.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ElipseshopDbContext : IdentityDbContext<User>
    {
        public ElipseshopDbContext()
            : base("name=ElipseshopEntities", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public virtual IDbSet<CategoryPicture> CategoryPictures { get; set; }
        public virtual IDbSet<Color> Colors { get; set; }
        public virtual IDbSet<ColorLanguage> ColorLanguages { get; set; }
        public virtual IDbSet<Language> Languages { get; set; }
        public virtual IDbSet<PCSQuantity> PCSQuantities { get; set; }
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<ProductLanguage> ProductLanguages { get; set; }
        public virtual IDbSet<ProductPicture> ProductPictures { get; set; }
        public virtual IDbSet<ShoppingBagItem> ShoppingBagItems { get; set; }
        public virtual IDbSet<Size> Sizes { get; set; }

        public static ElipseshopDbContext Create()
        {
            return new ElipseshopDbContext();
        }
        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();

            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in 
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
