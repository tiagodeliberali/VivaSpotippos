using System.Collections.Generic;
using VivaSpotippos.Model;
using VivaSpotippos.Stores;

namespace VivaSpotippos.Test
{
    public class PropertyStoreTestable : PropertyStore
    {
        public PropertyStoreTestable(IProvinceStore provinceStore) : base(provinceStore)
        {
        }

        public Dictionary<int, Property> GetPropertyDictionary()
        {
            return PropertyStore.properties;
        }

        public void ClearPropertyDictionary()
        {
            ResetPropertyDictionary();
        }
    }
}
