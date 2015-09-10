namespace ElipseshopMVC.Models
{
    using ElipseshopMVC.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ShoppingBagItem : AuditInfo, IDeletableEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int IDPCSQuantities { get; set; }
        [Required]
        public string IDUser { get; set; }
        [Required]
        public int Quantity { get; set; }

        [ForeignKey("IDPCSQuantities")]
        public virtual PCSQuantity PCSQuantity { get; set; }
        [ForeignKey("IDUser")]
        public virtual User User { get; set; }
        [Index]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
