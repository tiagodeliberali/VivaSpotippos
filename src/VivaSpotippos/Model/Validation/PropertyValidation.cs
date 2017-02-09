using System;
using System.Text;
using VivaSpotippos.Model.RestEntities;

namespace VivaSpotippos.Model.Validation
{
    public class PropertyValidation
    {
        public string ErrorMessage { get; private set; }

        public bool IsValid { get; private set; }

        public static PropertyValidation Validate(IPropertyData property)
        {
            var message = new StringBuilder();

            bool isValid = IsNull(message, property); ;

            isValid = isValid && IsInRange(message, property.x, 0, 1400, nameof(property.x));

            isValid = isValid && IsInRange(message, property.y, 0, 1000, nameof(property.y));

            isValid = isValid && IsInRange(message, property.beds, 1, 5, nameof(property.beds));

            isValid = isValid && IsInRange(message, property.baths, 1, 4, nameof(property.baths));

            isValid = isValid && IsInRange(message, property.squareMeters, 20, 240, nameof(property.squareMeters));

            return new PropertyValidation()
            {
                IsValid = isValid,
                ErrorMessage = message.ToString()
            };
        }

        private static bool IsNull(StringBuilder message, IPropertyData property)
        {
            bool isValid = property != null;

            if (!isValid)
            {
                message.AppendLine(ErrorMessages.NullIPropertyData);
            }

            return isValid;
        }

        private static bool IsInRange(StringBuilder message, int value, int startValue, int endValue, string propertyName)
        {
            var isValid = value >= startValue && value <= endValue;

            if (!isValid)
            {
                message.AppendLine(
                    string.Format(ErrorMessages.OutOfRange, 
                        propertyName, startValue, endValue));
            }

            return isValid;
        }
    }
}
