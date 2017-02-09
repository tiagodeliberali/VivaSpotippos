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
            var provinceStoreMock = new Mock<IProvinceStore>();
            provinceStoreMock
                .Setup(x => x.GetProvinces(It.IsAny<Position>()))
                .Returns(new List<Province>() { new Province() { name = DemoData.ProvinceName } });

            var data = DemoData.ValidPostRequest;

            var store = new PropertyStoreTestable(provinceStoreMock.Object);

            // Act
            var createdProperty = store.AddProperty(data);

            // Assert
            Assert.Equal(1, createdProperty.id);
            Assert.Contains(DemoData.ProvinceName, createdProperty.provinces);

            var storedProperties = store.GetPropertyDictionary();
            Assert.Equal(1, storedProperties.Count);
            Assert.True(storedProperties.ContainsKey(1));
        }
    }
}
