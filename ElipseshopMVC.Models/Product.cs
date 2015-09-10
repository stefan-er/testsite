namespace ElipseshopMVC.Models
{
    using ElipseshopMVC.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product : AuditInfo, IDeletableEntity
    {
        private ICollection<PCSQuantity> pcsQuantities;
        private ICollection<ProductLanguage> productLanguages;
        private ICollection<ProductPicture> productPictures;

        public Product()
        {
            this.pcsQuantities = new HashSet<PCSQuantity>();
            this.productLanguages = new HashSet<ProductLanguage>();
            this.productPictures = new HashSet<ProductPicture>();
        }

        [Key]
        public int ID { get; set; }
        public int? IDCategory { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }

        [ForeignKey("IDCategory")]
        public virtual Category Category { get; set; }
        public virtual ICollection<PCSQuantity> PCSQuantities
        {
            get { return this.pcsQuantities; }
            set { this.pcsQuantities = value; }
        }
        public virtual ICollection<ProductLanguage> ProductLanguages
        {
            get { return this.productLanguages; }
            set { this.productLanguages = value; }
        }
        public virtual ICollection<ProductPicture> ProductPictures 
        {
            get { return this.productPictures; }
            set { this.productPictures = value; } 
        }
        [Index]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
