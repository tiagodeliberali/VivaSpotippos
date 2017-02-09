using System.Collections.Generic;
using VivaSpotippos.Model.Entities;

namespace VivaSpotippos.Stores
{
    public interface IProvinceStore
    {
        List<Province> GetProvinces(Position position);
    }
}