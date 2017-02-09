using VivaSpotippos.Model;
using Xunit;

namespace VivaSpotippos.Test
{
    public class PropertyTest
    {
        [Fact]
        public void CreatePropertyFromIPropertyData()
        {
            // Arrange
            var data = DemoData.ValidIPropertyData;

            // Act
            var property = Property.CreateFrom(data);

            // Assert
            Assert.Equal(data.baths, property.baths);
            Assert.Equal(data.beds, property.beds);
            Assert.Equal(data.description, property.description);
            Assert.Equal(data.price, property.price);
            Assert.Equal(data.squareMeters, property.squareMeters);
            Assert.Equal(data.title, property.title);
            Assert.Equal(data.x, property.x);
            Assert.Equal(data.y, property.y);
        }
    }
}
