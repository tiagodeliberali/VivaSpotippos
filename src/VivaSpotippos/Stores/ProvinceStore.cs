using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using VivaSpotippos.Model.Entities;

namespace VivaSpotippos.Stores
{
    public class ProvinceStore : IProvinceStore
    {
        private static List<Province> provinces { get; set; }

        public ProvinceStore()
        {
            if (provinces == null)
            {
                LoadProvinces();
            }
        }

        private void LoadProvinces()
        {
            string provincesData = LoadProvinceData();

            provinces = new List<Province>();

            JObject provincesJObject = JObject.Parse(provincesData);

            foreach (var item in provincesJObject.Properties())
            {
                var newProvince = item.Value.ToObject<Province>();
                newProvince.Name = item.Name;

                provinces.Add(newProvince);
            }
        }

        /// <summary>
        /// This method can be replaced by versions that load fresh data from web or database
        /// </summary>
        /// <returns>A string representing the provinces json data</returns>
        private static string LoadProvinceData()
        {
            return File.ReadAllText("Data/provinces.json");
        }

        public List<Province> GetProvinces(Position position)
        {
            return provinces
                .Where(x => x.Boundaries.UpperLeft.X <= position.X && x.Boundaries.BottomRight.X >= position.X
                        && x.Boundaries.UpperLeft.Y >= position.Y && x.Boundaries.BottomRight.Y <= position.Y)
                .ToList();
        }
    }
}
