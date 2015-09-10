namespace ElipseshopMVC.Models
{
    using ElipseshopMVC.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PCSQuantity : AuditInfo, IDeletableEntity
    {
        public ICollection<ShoppingBagItem> shoppingBags;

        public PCSQuantity()
        {
            this.shoppingBags = new HashSet<ShoppingBagItem>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        public int IDProduct { get; set; }
        public int? IDColor { get; set; }
        public int? IDSize { get; set; }
        [Required]
        public int Quantity { get; set; }

        [ForeignKey("IDProduct")]
        public virtual Product Product { get; set; }
        [ForeignKey("IDColor")]        
        public virtual Color Color { get; set; }
        [ForeignKey("IDSize")]
        public virtual Size Size { get; set; }
        public virtual ICollection<ShoppingBagItem> ShoppingBags
        {
            get { return this.shoppingBags; }
            set { this.shoppingBags = value; }
        }
        [Index]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
