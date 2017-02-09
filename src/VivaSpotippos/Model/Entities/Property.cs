using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Model
{
    public class Property : IPropertyData
    {
        public int id { get; set; }
        public string title { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int beds { get; set; }
        public int baths { get; set; }
        public string[] provinces { get; set; }
        public int squareMeters { get; set; }
    }
}
