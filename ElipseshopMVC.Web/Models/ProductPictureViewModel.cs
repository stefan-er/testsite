namespace ElipseshopMVC.Web.Models
{
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure.Mapping;

    public class ProductPictureViewModel : IMapFrom<ProductPicture>
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}