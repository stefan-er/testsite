namespace ElipseshopMVC.Web.Models
{
    using ElipseshopMVC.Data;
    using ElipseshopMVC.Models;
    using ElipseshopMVC.Web.Infrastructure.Mapping;

    public class SizeViewModel : IMapFrom<Size>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (this != obj)
            {
                SizeViewModel sizeViewModel = obj as SizeViewModel;

                if (sizeViewModel == null)
                {
                    return false;
                }
                if (this.ID != sizeViewModel.ID)
                {
                    return false;
                }
            }

            return true;
        }
        public override int GetHashCode()
        {
            int result = this.ID.GetHashCode()
                ^ (this.Name != null ? this.Name.GetHashCode() : 2);

            return result;
        }
    }
}