namespace ElipseshopMVC.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CategoryLanguage
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int IDCategory { get; set; }
        [Required]
        public int IDLanguage { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey("IDCategory")]
        public virtual Category Category { get; set; }
        [ForeignKey("IDLanguage")]
        public virtual Language Language { get; set; }
    }

}
