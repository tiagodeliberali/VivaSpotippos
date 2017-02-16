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
            Assert.Equal(data.baths, property.Baths);
            Assert.Equal(data.beds, property.Beds);
            Assert.Equal(data.description, property.Description);
            Assert.Equal(data.price, property.Price);
            Assert.Equal(data.squareMeters, property.SquareMeters);
            Assert.Equal(data.title, property.Title);
            Assert.Equal(data.x, property.X);
            Assert.Equal(data.y, property.Y);
        }
    }
}
