namespace VivaSpotippos.Model.RestEntities
{
    public interface IPropertyData
    {
        int x { get; set; }
        int y { get; set; }
        string title { get; set; }
        int price { get; set; }
        string description { get; set; }
        int beds { get; set; }
        int baths { get; set; }
        int squareMeters { get; set; }
    }
}
