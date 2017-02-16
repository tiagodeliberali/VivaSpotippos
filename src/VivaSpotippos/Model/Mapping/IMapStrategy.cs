using System.Collections.Generic;
using VivaSpotippos.Model.Entities;

namespace VivaSpotippos.Model.Mapping
{
    public interface IMapStrategy
    {
        List<Property> GetOnMap(Position startPosition, Position endPosition);
        void AddToMap(Property property);
        bool PositionOnMapIsFree(int x, int y);
        void ResetMap();
    }
}
