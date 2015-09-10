namespace ElipseshopMVC.Models
{
    using ElipseshopMVC.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Color : AuditInfo, IDeletableEntity
    {
        public ICollection<ColorLanguage> colorLanguages;
        public ICollection<PCSQuantity> pcsQuantities;

        public Color()
        {
            this.colorLanguages = new HashSet<ColorLanguage>();
            this.pcsQuantities = new HashSet<PCSQuantity>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        public string HexCode { get; set; }

        public virtual ICollection<ColorLanguage> ColorLanguages 
        { 
            get { return this.colorLanguages; } 
            set { this.colorLanguages = value; } 
        }
        public virtual ICollection<PCSQuantity> PCSQuantities
        {
            get { return this.pcsQuantities; }
            set { this.pcsQuantities = value; }
        }
        [Index]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
