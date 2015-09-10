namespace ElipseshopMVC.Models
{
    using ElipseshopMVC.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CategoryPicture : AuditInfo, IDeletableEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int IDCategory { get; set; }
        [Required]
        public string Url { get; set; }
        public bool IsMain { get; set; }

        [ForeignKey("IDCategory")]
        public virtual Category Category { get; set; }
        [Index]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
