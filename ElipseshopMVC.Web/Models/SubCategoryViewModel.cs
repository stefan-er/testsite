namespace ElipseshopMVC.Web.Models
{
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure;
    using ElipseshopMVC.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class SubCategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public string Name { get; set; }
        public IEnumerable<ProductCategoryViewModel> Products { get; set; }
        public SiteMapNode StarttingNode { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            Languages language = Languages.EN;

            configuration.CreateMap<Category, TopCategoryViewModel>()
                .ForMember(vm => vm.Name, options => options
                    .MapFrom(m => m.CategoryLanguages.FirstOrDefault(x => x.IDLanguage == (int)language)
                        .Name));
        }
    }
}