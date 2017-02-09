﻿using System.Collections.Generic;
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

        public PropertyStore(IProvinceStore provinceStore)
        {
            this.provinceStore = provinceStore;

            if (properties == null)
            {
                properties = new Dictionary<int, Property>();
                memoryIdentity = 1;
            }
        }

        public Property AddProperty(IPropertyData data)
        {
            var property = Property.CreateFrom(data);

            property.id = memoryIdentity++;

            property.provinces = provinceStore
                .GetProvinces(new Position(data.x, data.y))
                .Select(x => x.name)
                .ToArray();

            properties.Add(property.id, property);

            return property;
        }
    }
}