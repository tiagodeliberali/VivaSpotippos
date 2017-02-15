using System;
using System.Collections.Generic;
using System.Linq;
using VivaSpotippos.Model;
using VivaSpotippos.Model.Entities;
using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Stores
{
    public class PropertyStore : IPropertyStore
    {
        private IProvinceStore provinceStore;
        private static int memoryIdentity = 1;
        protected static Dictionary<int, Property> properties;
        protected static Property[,] map;

        public PropertyStore(IProvinceStore provinceStore)
        {
            this.provinceStore = provinceStore;

            if (properties == null)
            {
                ResetPropertyStorage();
            }
        }

        protected void ResetPropertyStorage()
        {
            properties = new Dictionary<int, Property>();
            map = new Property[VivaSettings.MaxMapX, VivaSettings.MaxMapY];

            memoryIdentity = 1;
        }

        public Property AddProperty(PropertyPostRequest data)
        {
            if (!PositionOnMapIsFree(data.x, data.y))
            {
                throw new PropertyStoreAddException(
                    string.Format(ErrorMessages.PositionAlreadyAllocated, data.x, data.y));
            }

            var property = Property.CreateFrom(data);

            property.id = memoryIdentity++;

            property.provinces = provinceStore
                .GetProvinces(new Position(data.x, data.y))
                .Select(x => x.name)
                .ToArray();

            properties.Add(property.id, property);

            AddToMap(property);

            return property;
        }

        private static void AddToMap(Property property)
        {
            map[property.x, property.y] = property;
        }

        private bool PositionOnMapIsFree(int x, int y)
        {
            return map[x, y] == null;
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
            var foundProperties = new List<Property>();

            for (int xPosition = startPosition.x; xPosition <= endPosition.x; xPosition++)
            {
                for (int yPosition = startPosition.y; yPosition <= endPosition.y; yPosition++)
                {
                    if (map[xPosition, yPosition] != null)
                    {
                        foundProperties.Add(map[xPosition, yPosition]);
                    }
                }
            }

            return foundProperties;
        }
    }
}
