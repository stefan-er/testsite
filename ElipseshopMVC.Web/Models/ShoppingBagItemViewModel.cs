namespace ElipseshopMVC.Web.Models
{
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure;
    using ElipseshopMVC.Web.Infrastructure.Mapping;
    using System;
    using System.Linq;

    public class ShoppingBagItemViewModel : IMapFrom<ShoppingBagItem>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string IDUser { get; set; }
        public int IDPCSQuantities { get; set; }
        public int Quantity { get; set; }
        public string ProductPictureUrl { get; set; }
        public string ProductTitle { get; set; }
        public ColorViewModel ProductColor { get; set; }
        public SizeViewModel ProductSize { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            Languages language = Languages.EN;

            configuration.CreateMap<ShoppingBagItem, ShoppingBagItemViewModel>()
                .ForMember(vm => vm.ProductPictureUrl, options => options
                    .MapFrom(m => m.PCSQuantity.Product.ProductPictures.FirstOrDefault(x => x.IsMain)
                        .Url));

            configuration.CreateMap<ShoppingBagItem, ShoppingBagItemViewModel>()
                .ForMember(vm => vm.ProductTitle, options => options
                    .MapFrom(m => m.PCSQuantity.Product.ProductLanguages.FirstOrDefault(x => x.IDLanguage == (int)language)
                        .Title));

            configuration.CreateMap<ShoppingBagItem, ShoppingBagItemViewModel>()
                .ForMember(vm => vm.ProductColor, options => options
                    .MapFrom(m => m.PCSQuantity.Color.ColorLanguages.FirstOrDefault(x => x.IDLanguage == (int)language)
                        .Name));

            configuration.CreateMap<ShoppingBagItem, ShoppingBagItemViewModel>()
                .ForMember(vm => vm.ProductSize, options => options
                    .MapFrom(m => m.PCSQuantity.Size.Name));
        }
    }
}