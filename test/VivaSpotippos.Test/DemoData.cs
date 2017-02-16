using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VivaSpotippos.Model;
using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Test
{
    public class DemoData
    {
        public static string ProvinceName {
            get
            {
                return "ProvinceName";
            }
        }

        public static PropertyPostRequest ValidPostRequest
        {
            get
            {
                return new PropertyPostRequest()
                {
                    beds = 4,
                    baths = 3,
                    description = "description",
                    price = 1250000,
                    squareMeters = 100,
                    title = "title",
                    x = 222,
                    y = 444
                };
            }
        }

        public static void LoadDemoData(PropertyStoreTestable store)
        {
            var data1 = DemoData.ValidPostRequest;
            data1.x = 10;
            data1.y = 10;

            var data2 = DemoData.ValidPostRequest;
            data2.x = 11;
            data2.y = 11;

            var data3 = DemoData.ValidPostRequest;
            data3.x = 12;
            data3.y = 12;

            var data4 = DemoData.ValidPostRequest;
            data4.x = 13;
            data4.y = 13;

            var data5 = DemoData.ValidPostRequest;
            data5.x = 14;
            data5.y = 14;

            store.AddProperty(data1);
            store.AddProperty(data2);
            store.AddProperty(data3);
            store.AddProperty(data4);
            store.AddProperty(data5);
        }
    }
}
