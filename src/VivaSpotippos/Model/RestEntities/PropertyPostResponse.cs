namespace VivaSpotippos.Model.RestEntities
{
    public class PropertyPostResponse
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public Property CreatedProperty { get; set; }
    }
}
