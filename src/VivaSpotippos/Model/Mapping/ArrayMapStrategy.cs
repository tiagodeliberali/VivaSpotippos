using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VivaSpotippos.Model.Entities;

namespace VivaSpotippos.Model.Mapping
{
    public class ArrayMapStrategy : IMapStrategy
    {
        protected static Property[,] map;

        public List<Property> GetOnMap(Position startPosition, Position endPosition)
        {
            var foundProperties = new List<Property>();

            for (int xPosition = startPosition.X; xPosition <= endPosition.X; xPosition++)
            {
                for (int yPosition = startPosition.Y; yPosition <= endPosition.Y; yPosition++)
                {
                    if (map[xPosition, yPosition] != null)
                    {
                        foundProperties.Add(map[xPosition, yPosition]);
                    }
                }
            }

            return foundProperties;
        }

        public void AddToMap(Property property)
        {
            map[property.X, property.Y] = property;
        }

        public bool PositionOnMapIsFree(int x, int y)
        {
            return map[x, y] == null;
        }

        public void ResetMap()
        {
            map = new Property[VivaSettings.MaxMapX, VivaSettings.MaxMapY];
        }
    }
}
