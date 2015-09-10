namespace ElipseshopMVC.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ColorLanguage
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int IDColor { get; set; }
        [Required]
        public int IDLanguage { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey("IDColor")]
        public virtual Color Color { get; set; }
        [ForeignKey("IDLanguage")]        
        public virtual Language Language { get; set; }
    }
}
