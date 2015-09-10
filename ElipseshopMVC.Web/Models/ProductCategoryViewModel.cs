namespace ElipseshopMVC.Web.Models
{
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure;
    using ElipseshopMVC.Web.Infrastructure.Mapping;
    using System.Linq;
    using System.Web;

    public class ProductCategoryViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        public ProductPictureViewModel MainPicture { get; set; }
        public SiteMapNode StarttingNode { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            Languages language = Languages.EN;

            configuration.CreateMap<Product, ProductCategoryViewModel>()
                .ForMember(vm => vm.Title,options => options
                    .MapFrom(m => m.ProductLanguages.FirstOrDefault(x => x.IDLanguage == (int)language)
                        .Title));

            configuration.CreateMap<Product, ProductCategoryViewModel>()
                .ForMember(vm => vm.MainPicture, options => options
                    .MapFrom(m => m.ProductPictures.FirstOrDefault(x => x.IsMain == true)));
        }
    }
}