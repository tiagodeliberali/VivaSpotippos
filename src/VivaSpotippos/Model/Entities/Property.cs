using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Model
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public string[] Provinces { get; set; }
        public int SquareMeters { get; set; }

        public static Property CreateFrom(PropertyPostRequest data)
        {
            return new Property()
            {
                Baths = data.baths,
                Beds = data.beds,
                Description = data.description,
                Price = data.price,
                SquareMeters = data.squareMeters,
                Title = data.title,
                X = data.x,
                Y = data.y
            };
        }
    }
}
