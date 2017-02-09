using System.Collections.Generic;
using Moq;
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
