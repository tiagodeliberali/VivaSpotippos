using System.Collections.Generic;
using Moq;
using VivaSpotippos.Model;
using VivaSpotippos.Model.Entities;
using VivaSpotippos.Stores;
using Xunit;

namespace VivaSpotippos.Test
{
    public class PropertyStoreTest
    {
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
            Assert.Equal(1, createdProperty.id);
            Assert.Contains(DemoData.ProvinceName, createdProperty.provinces);

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
            string expectedExeption = string.Format(ErrorMessages.PositionAlreadyAllocated, data.x, data.y);

            Assert.Equal(typeof(PropertyStoreAddException), exeption.GetType());
            Assert.Equal(expectedExeption, exeption.Message);
        }

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

        [Fact]
        public void GetPropertyRegistered()
        {
            // Arrange
            var store = GetPropertyStore();

            var data = DemoData.ValidPostRequest;

            var createdProperty = store.AddProperty(data);

            // Act
            var property = store.Get(createdProperty.id);

            // Assert
            Assert.NotNull(property);
            Assert.Equal(createdProperty.id, property.id);
        }

        private static PropertyStoreTestable GetPropertyStore()
        {
            var provinceStoreMock = new Mock<IProvinceStore>();
            provinceStoreMock
                .Setup(x => x.GetProvinces(It.IsAny<Position>()))
                .Returns(new List<Province>() { new Province() { name = DemoData.ProvinceName } });

            var store = new PropertyStoreTestable(provinceStoreMock.Object);
            return store;
        }
    }
}
