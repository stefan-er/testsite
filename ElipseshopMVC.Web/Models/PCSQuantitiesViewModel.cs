namespace ElipseshopMVC.Web.Models
{
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure.Mapping;

    public class PCSQuantitiesViewModel : IMapFrom<PCSQuantity>
    {
        public int ID { get; set; }
        public ColorViewModel Color { get; set; }
        public SizeViewModel Size { get; set; }
        public int Quantity { get; set; }
    }
}