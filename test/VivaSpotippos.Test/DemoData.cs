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

        public static IPropertyData ValidIPropertyData
        {
            get
            {
                return new Property()
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
    }
}
