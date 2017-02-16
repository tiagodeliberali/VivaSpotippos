using System.Collections.Generic;
using VivaSpotippos.Model;
using VivaSpotippos.Model.Mapping;
using VivaSpotippos.Stores;

namespace VivaSpotippos.Test
{
    public class PropertyStoreTestable : PropertyStore
    {
        public PropertyStoreTestable(IProvinceStore provinceStore, IMapStrategy mapStrategy) : base(provinceStore, mapStrategy)
        {
        }

        public Dictionary<int, Property> GetPropertyDictionary()
        {
            return PropertyStore.properties;
        }

        public void Clear()
        {
            ResetPropertyStorage();
        }
    }
}
