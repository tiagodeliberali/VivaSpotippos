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
            var property = DemoData.ValidPostRequest;

            // Act
            var validation = PropertyValidation.Validate(property);

            // Assert
            Assert.True(validation.IsValid);
        }

        [Fact]
        public void NullObjectIsNotValid()
        {
            // Arrange / Act
            var validation = PropertyValidation.Validate(null);

            // Assert
            Assert.False(validation.IsValid);
            Assert.Contains(ErrorMessages.NullIPropertyData, validation.ErrorMessage);
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
            var property = DemoData.ValidPostRequest;
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
            var property = DemoData.ValidPostRequest;
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
            var property = DemoData.ValidPostRequest;
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
            var property = DemoData.ValidPostRequest;
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

        
    }
}
