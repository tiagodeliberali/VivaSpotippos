using System;

namespace VivaSpotippos.Model
{
    public class SystemMessages
    {
        public static string Created
        {
            get
            {
                return "Created";
            }
        }

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

        public static string PositionAlreadyAllocated
        {
            get
            {
                return "There is already a property at position '{0}, {1}'. It is not possible to add a new position to the same location.";
            }
        }
    }
}
