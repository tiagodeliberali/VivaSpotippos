using VivaSpotippos.Model;
using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Stores
{
    public interface IPropertyStore
    {
        Property AddProperty(PropertyPostRequest data);
    }
}