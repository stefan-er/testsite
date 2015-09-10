namespace ElipseshopMVC.Models
{
    using ElipseshopMVC.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Language : AuditInfo, IDeletableEntity
    {
        public ICollection<CategoryLanguage> categoryLanguages;
        public ICollection<ColorLanguage> colorLanguages;
        public ICollection<ProductLanguage> productLanguages;

        public Language()
        {
            this.categoryLanguages = new HashSet<CategoryLanguage>();
            this.colorLanguages = new HashSet<ColorLanguage>();
            this.productLanguages = new HashSet<ProductLanguage>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }

        public virtual ICollection<CategoryLanguage> CategoryLanguages {
            get { return this.categoryLanguages; }
            set { this.categoryLanguages = value ; }
        }
        public virtual ICollection<ColorLanguage> ColorLanguages
        {
            get { return this.colorLanguages; }
            set { this.colorLanguages = value; }
        }
        public virtual ICollection<ProductLanguage> ProductLanguages
        {
            get { return this.productLanguages; }
            set { this.productLanguages = value; }
        }
        [Index]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
