using System.Collections.Generic;
using Moq;
using VivaSpotippos.Model;
using VivaSpotippos.Model.Entities;
using VivaSpotippos.Model.Mapping;
using VivaSpotippos.Stores;
using Xunit;

namespace VivaSpotippos.Test
{
    public class PropertyStoreTest
    {
        /// <summary>
        /// Given a property post request
        /// When adds it to the property store
        /// Then it should create a property from it, adding id and provinces realted to its position
        /// </summary>
        [Fact]
        public void AddPropertyShouldIncludeIdAndProvinces()
        {
            // Arrange
            var store = GetPropertyStore();

            store.Clear();

            var data = DemoData.ValidPostRequest;

            // Act
            var createdProperty = store.AddProperty(data);

            // Assert
            Assert.Equal(1, createdProperty.Id);
            Assert.Contains(DemoData.ProvinceName, createdProperty.Provinces);

            var storedProperties = store.GetPropertyDictionary();
            Assert.Equal(1, storedProperties.Count);
            Assert.True(storedProperties.ContainsKey(1));
        }

        /// <summary>
        /// Given a property registered on the store
        /// When I try to add a new property to the same position
        /// Then it should thrown an PropertyStoreAddException
        /// </summary>
        [Fact]
        public void ShouldNotAllowAddPropertiesOnSamePosition()
        {
            // Arrange
            var store = GetPropertyStore();

            store.Clear();

            var data = DemoData.ValidPostRequest;

            store.AddProperty(data);

            // Act
            var exeption = Record.Exception(() => store.AddProperty(data));

            // Assert
            string expectedExeption = string.Format(SystemMessages.PositionAlreadyAllocated, data.x, data.y);

            Assert.Equal(typeof(PropertyStoreAddException), exeption.GetType());
            Assert.Equal(expectedExeption, exeption.Message);
        }

        /// <summary>
        /// Given a invalid property id
        /// When gets property by the invalid id
        /// Then it should return null
        /// </summary>
        [Fact]
        public void GetPropertyNotRegistered()
        {
            // Arrange
            var store = GetPropertyStore();

            int invalidId = 567;

            // Act
            var property = store.Get(invalidId);

            // Assert
            Assert.Null(property);
        }

        /// <summary>
        /// Given a property id
        /// When gets property by the id
        /// Then it should return the related property
        /// </summary>
        [Fact]
        public void GetPropertyRegistered()
        {
            // Arrange
            var store = GetPropertyStore();

            var data = DemoData.ValidPostRequest;

            var createdProperty = store.AddProperty(data);

            // Act
            var property = store.Get(createdProperty.Id);

            // Assert
            Assert.NotNull(property);
            Assert.Equal(createdProperty.Id, property.Id);
        }

        /// <summary>
        /// Given a set of stored properties
        /// When gets properties by position
        /// Then it should return all properties inside the selected area
        /// </summary>
        [Fact]
        public void GetPropertiesOnMap()
        {
            // Arrange
            var store = GetPropertyStore();

            DemoData.LoadDemoData(store);

            // Act
            var propertyList = store.Get(new Position(10, 10), new Position(12, 12));

            // Assert
            Assert.Equal(3, propertyList.Count);
        }

        private static PropertyStoreTestable GetPropertyStore()
        {
            var provinceStoreMock = new Mock<IProvinceStore>();
            provinceStoreMock
                .Setup(x => x.GetProvinces(It.IsAny<Position>()))
                .Returns(new List<Province>() { new Province() { Name = DemoData.ProvinceName } });

            var store = new PropertyStoreTestable(provinceStoreMock.Object, new ArrayMapStrategy());
            return store;
        }
    }
}
