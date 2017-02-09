using System;

namespace VivaSpotippos.Model.Validation
{
    public class ErrorMessages
    {
        public static string NullIPropertyData
        {
            get
            {
                return "Null IPropertyData";
            }
        }

        public static string OutOfRange
        {
            get
            {
                return "The property '{0}' is outside the specified range of '{1}' and '{2}'";
            }
        }
    }
}
