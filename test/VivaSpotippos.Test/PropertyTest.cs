using VivaSpotippos.Model;
using Xunit;

namespace VivaSpotippos.Test
{
    public class PropertyTest
    {
        /// <summary>
        /// Given a property post request instance
        /// When builds a property from it
        /// Then the property should contais all property post request data
        /// </summary>
        [Fact]
        public void CreatePropertyFromIPropertyData()
        {
            // Arrange
            var data = DemoData.ValidPostRequest;

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
