using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VivaSpotippos.Model;
using VivaSpotippos.Model.RestEntities;
using VivaSpotippos.Model.Validation;
using Xunit;

namespace VivaSpotippos.Test
{
    public class PropertyValidationTest
    {
        [Fact]
        public void IsValid()
        {
            // Arrange
            var property = GetValidProperty();

            // Act
            var validation = PropertyValidation.Validate(property);

            // Assert
            Assert.True(validation.IsValid);
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(700, 700, true)]
        [InlineData(1400, 1000, true)]
        [InlineData(1401, 1000, false)]
        [InlineData(1400, 1001, false)]
        [InlineData(1401, 1001, false)]
        public void ValidatePropertyPosition(int x, int y, bool isValid)
        {
            // Arrange
            var property = GetValidProperty();
            property.x = x;
            property.y = y;

            // Act
            var validation = PropertyValidation.Validate(property);

            // Assert
            Assert.Equal(isValid, validation.IsValid);

            if (!isValid)
            {
                Assert.True(validation.ErrorMessage.Contains("'x'") || validation.ErrorMessage.Contains("'y'"));
            }
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        public void ValidatePropertyBedNumber(int value, bool isValid)
        {
            // Arrange
            var property = GetValidProperty();
            property.beds = value;

            // Act
            var validation = PropertyValidation.Validate(property);

            // Assert
            Assert.Equal(isValid, validation.IsValid);

            if (!isValid)
            {
                Assert.Contains("'beds'", validation.ErrorMessage);
            }
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        public void ValidatePropertyBathNumber(int value, bool isValid)
        {
            // Arrange
            var property = GetValidProperty();
            property.baths = value;

            // Act
            var validation = PropertyValidation.Validate(property);

            // Assert
            Assert.Equal(isValid, validation.IsValid);

            if (!isValid)
            {
                Assert.Contains("'baths'", validation.ErrorMessage);
            }
        }

        [Theory]
        [InlineData(19, false)]
        [InlineData(20, true)]
        [InlineData(240, true)]
        [InlineData(241, false)]
        public void ValidatePropertySquareMeters(int value, bool isValid)
        {
            // Arrange
            var property = GetValidProperty();
            property.squareMeters = value;

            // Act
            var validation = PropertyValidation.Validate(property);

            // Assert
            Assert.Equal(isValid, validation.IsValid);

            if (!isValid)
            {
                Assert.Contains("'squareMeters'", validation.ErrorMessage);
            }
        }

        private IPropertyData GetValidProperty()
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
