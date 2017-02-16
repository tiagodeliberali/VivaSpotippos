using System;
using System.Text;
using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Model.Validation
{
    public class PropertyValidation
    {
        public string ErrorMessage { get; private set; }

        public bool IsValid { get; private set; }

        public static PropertyValidation Validate(PropertyPostRequest property)
        {
            var message = new StringBuilder();

            bool isValid = IsNull(message, property); ;

            isValid = isValid 
                && IsInRange(message, 
                    property.x, VivaSettings.MinMapX, VivaSettings.MaxMapX, nameof(property.x));

            isValid = isValid 
                && IsInRange(message, 
                    property.y, VivaSettings.MinMapY, VivaSettings.MaxMapY, nameof(property.y));

            isValid = isValid 
                && IsInRange(message, 
                    property.beds, VivaSettings.MinBedsNumber, VivaSettings.MaxBedsNumber, nameof(property.beds));

            isValid = isValid 
                && IsInRange(message, 
                    property.baths, VivaSettings.MinBathsNumber, VivaSettings.MaxBathsNumber, nameof(property.baths));

            isValid = isValid 
                && IsInRange(message, 
                    property.squareMeters, VivaSettings.MinPropertySize, VivaSettings.MaxPropertySize, nameof(property.squareMeters));

            return new PropertyValidation()
            {
                IsValid = isValid,
                ErrorMessage = message.ToString()
            };
        }

        private static bool IsNull(StringBuilder message, PropertyPostRequest property)
        {
            bool isValid = property != null;

            if (!isValid)
            {
                message.AppendLine(SystemMessages.NullIPropertyData);
            }

            return isValid;
        }

        private static bool IsInRange(StringBuilder message, int value, int startValue, int endValue, string propertyName)
        {
            var isValid = value >= startValue && value <= endValue;

            if (!isValid)
            {
                message.AppendLine(
                    string.Format(SystemMessages.OutOfRange, 
                        propertyName, startValue, endValue));
            }

            return isValid;
        }
    }
}
