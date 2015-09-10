namespace ElipseshopMVC.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductOrderViewModel
    {
        [Required]
        public int ProductQuantity { get; set; }
        [Required]
        public int PCSQuantityID { get; set; }
    }
}