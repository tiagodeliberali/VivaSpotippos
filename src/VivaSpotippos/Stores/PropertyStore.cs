using System.Collections.Generic;
using System.Linq;
using VivaSpotippos.Model;
using VivaSpotippos.Model.Entities;
using VivaSpotippos.Model.Mapping;
using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Stores
{
    public class PropertyStore : IPropertyStore
    {
        private IProvinceStore provinceStore;
        private static int memoryIdentity = 1;
        private IMapStrategy mapStrategy;
        protected static Dictionary<int, Property> properties;
        
        public PropertyStore(IProvinceStore provinceStore, IMapStrategy mapStrategy)
        {
            this.provinceStore = provinceStore;
            this.mapStrategy = mapStrategy;

            if (properties == null)
            {
                ResetPropertyStorage();
            }
        }

        protected void ResetPropertyStorage()
        {
            properties = new Dictionary<int, Property>();
            mapStrategy.ResetMap();

            memoryIdentity = 1;
        }

        public Property AddProperty(PropertyPostRequest data)
        {
            if (!mapStrategy.PositionOnMapIsFree(data.x, data.y))
            {
                throw new PropertyStoreAddException(
                    string.Format(SystemMessages.PositionAlreadyAllocated, data.x, data.y));
            }

            var property = Property.CreateFrom(data);

            property.id = memoryIdentity++;

            property.provinces = provinceStore
                .GetProvinces(new Position(data.x, data.y))
                .Select(x => x.name)
                .ToArray();

            properties.Add(property.id, property);

            mapStrategy.AddToMap(property);

            return property;
        }

        public Property Get(int id)
        {
            if (properties.ContainsKey(id))
            {
                return properties[id];
            }

            return null;
        }

        public List<Property> Get(Position startPosition, Position endPosition)
        {
            return mapStrategy.GetOnMap(startPosition, endPosition);
        }
    }
}
