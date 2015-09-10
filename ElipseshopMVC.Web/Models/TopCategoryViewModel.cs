namespace ElipseshopMVC.Web.Models
{
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure;
    using ElipseshopMVC.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class TopCategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public string Name { get; set; }
        public CategoryPictureViewModel MainPicture { get; set; }
        public IEnumerable<CategoryPictureViewModel> CategoryPictures { get; set; }
        public SiteMapNode StarttingNode { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            Languages language = Languages.EN;

            configuration.CreateMap<Category, TopCategoryViewModel>()
                .ForMember(vm => vm.Name, options => options
                    .MapFrom(m => m.CategoryLanguages.FirstOrDefault(x => x.IDLanguage == (int)language)
                        .Name));

            configuration.CreateMap<Category, TopCategoryViewModel>()
                .ForMember(vm => vm.MainPicture, options => options
                    .MapFrom(m => m.CategoryPictures.FirstOrDefault(x => x.IsMain == true)));
        }
    }
}