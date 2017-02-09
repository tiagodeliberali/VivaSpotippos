using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Model
{
    public class Property
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

        public static Property CreateFrom(PropertyPostRequest data)
        {
            return new Property()
            {
                baths = data.baths,
                beds = data.beds,
                description = data.description,
                price = data.price,
                squareMeters = data.squareMeters,
                title = data.title,
                x = data.x,
                y = data.y
            };
        }
    }
}
