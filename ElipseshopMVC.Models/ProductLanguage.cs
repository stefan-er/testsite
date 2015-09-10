namespace ElipseshopMVC.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductLanguage
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int IDProduct { get; set; }
        [Required]
        public int IDLanguage { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("IDProduct")]
        public virtual Product Product { get; set; }
        [ForeignKey("IDLanguage")]
        public virtual Language Language { get; set; }
    }
}
