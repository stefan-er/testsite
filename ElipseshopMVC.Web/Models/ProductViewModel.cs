namespace ElipseshopMVC.Web.Models
{
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure;
    using ElipseshopMVC.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int? IDCategory { get; set; }
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        public string Description { get; set; }
        public ProductPictureViewModel MainPicture { get; set; }
        public IEnumerable<ProductPictureViewModel> ProductPictures { get; set; }
        public IEnumerable<PCSQuantitiesViewModel> PCSQuantities { get; set; }
        public IList<SelectListItem> ProductColors { get; set; }
        public IList<SelectListItem> ProductSizes { get; set; }
        public IList<SelectListItem> ProductQuantities { get; set; }
        public int SelectedPCSQuantityID { get; set; }
        public SiteMapNode StarttingNode { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            Languages language = Languages.EN;

            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(vm => vm.Title, options => options
                    .MapFrom(m => m.ProductLanguages.FirstOrDefault(x => x.IDLanguage == (int)language)
                        .Title));

            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(vm => vm.Description, options => options
                    .MapFrom(p => p.ProductLanguages.FirstOrDefault(x => x.IDLanguage == (int)language)
                        .Description));

            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(vm => vm.MainPicture, options => options
                    .MapFrom(m => m.ProductPictures.FirstOrDefault(x => x.IsMain == true)));
        }
    }
}