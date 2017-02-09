using System.Collections.Generic;
using VivaSpotippos.Model;
using VivaSpotippos.Model.Entities;
using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Stores
{
    public interface IPropertyStore
    {
        Property AddProperty(PropertyPostRequest data);
        Property Get(int id);
        List<Property> Get(Position startPosition, Position endPosition);
    }
}