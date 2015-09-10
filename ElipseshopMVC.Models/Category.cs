namespace ElipseshopMVC.Models
{
    using ElipseshopMVC.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category : AuditInfo, IDeletableEntity
    {
        private ICollection<Category> childCategories;
        private ICollection<CategoryLanguage> categoryLanguages;
        private ICollection<CategoryPicture> categoryPictures;
        private ICollection<Product> products;

        public Category()
        {
            this.childCategories = new HashSet<Category>();
            this.categoryLanguages = new HashSet<CategoryLanguage>();
            this.categoryPictures = new HashSet<CategoryPicture>();
            this.products = new HashSet<Product>();
        }

        [Key]
        public int ID { get; set; }
        public int? IDParentCategory { get; set; }

        [ForeignKey("IDParentCategory")]
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> ChildCategories
        {
            get { return this.childCategories; }
            set { this.childCategories = value; }
        }
        public virtual ICollection<CategoryLanguage> CategoryLanguages
        {
            get { return this.categoryLanguages; }
            set { this.categoryLanguages = value; }
        }
        public virtual ICollection<CategoryPicture> CategoryPictures
        {
            get { return this.categoryPictures; }
            set { this.categoryPictures = value; }
        }
        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
        [Index]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
