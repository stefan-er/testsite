namespace ElipseshopMVC.Models
{
    using ElipseshopMVC.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductPicture : AuditInfo, IDeletableEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int IDProduct { get; set; }
        [Required]
        public string Url { get; set; }
        public bool IsMain { get; set; }

        [ForeignKey("IDProduct")]
        public virtual Product Product { get; set; }
        [Index]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
