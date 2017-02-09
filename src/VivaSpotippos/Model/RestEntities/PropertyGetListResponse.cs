using System.Collections.Generic;

namespace VivaSpotippos.Model.RestEntities
{
    public class PropertyGetListResponse
    {
        public int foundProperties { get; set; }
        public List<Property> properties { get; set; }
    }
}
