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

            bool isValid = true;

            isValid = IsInRange(message, property.x, 0, 1400, nameof(property.x));

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

        private static bool IsInRange(StringBuilder message, int value, int startValue, int endValue, string propertyName)
        {
            var isValid = value >= startValue && value <= endValue;

            if (!isValid)
            {
                message.AppendLine(
                    string.Format("The property '{0}' is outside the specified range of '{1}' and '{2}'", 
                        propertyName, startValue, endValue));
            }

            return isValid;
        }
    }
}
