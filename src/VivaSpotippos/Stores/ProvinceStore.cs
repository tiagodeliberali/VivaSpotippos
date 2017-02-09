using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using VivaSpotippos.Model.Entities;

namespace VivaSpotippos.Stores
{
    public class ProvinceStore
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
                newProvince.name = item.Name;

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
                .Where(x => x.boundaries.upperLeft.x <= position.x && x.boundaries.bottomRight.x >= position.x
                        && x.boundaries.upperLeft.y >= position.y && x.boundaries.bottomRight.y <= position.y)
                .ToList();
        }
    }
}
