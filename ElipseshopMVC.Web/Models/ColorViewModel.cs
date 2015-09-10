namespace ElipseshopMVC.Web.Models
{
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure;
    using ElipseshopMVC.Web.Infrastructure.Mapping;
    using System.Linq;

    public class ColorViewModel : IMapFrom<Color>, IHaveCustomMappings
    {
        public int ID { get; set; }
        public string HexCode { get; set; }
        public string Name { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            Languages language = Languages.EN;

            configuration.CreateMap<Color, ColorViewModel>()
                .ForMember(vm => vm.Name, options => options
                    .MapFrom(m => m.ColorLanguages.FirstOrDefault(x => x.IDLanguage == (int)language)
                        .Name));
        }
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ColorViewModel)obj);
        }
        public override int GetHashCode()
        {
            int result = this.ID.GetHashCode() ^ (this.HexCode != null ? this.HexCode.GetHashCode() : 2);

            return result;
        }

        protected bool Equals(ColorViewModel other)
        {
            return int.Equals(this.ID, other.ID);
        }
    }
}