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

        [Fact]
        public void GetPropertiesOnMap()
        {
            // Arrange
            var store = GetPropertyStore();

            LoadDemoData(store);

            // Act
            var propertyList = store.Get(new Position(10, 10), new Position(12, 12));

            // Assert
            Assert.Equal(3, propertyList.Count);
        }

        private static void LoadDemoData(PropertyStoreTestable store)
        {
            var data1 = DemoData.ValidPostRequest;
            data1.x = 10;
            data1.y = 10;

            var data2 = DemoData.ValidPostRequest;
            data2.x = 11;
            data2.y = 11;

            var data3 = DemoData.ValidPostRequest;
            data3.x = 12;
            data3.y = 12;

            var data4 = DemoData.ValidPostRequest;
            data4.x = 13;
            data4.y = 13;

            var data5 = DemoData.ValidPostRequest;
            data5.x = 14;
            data5.y = 14;

            store.AddProperty(data1);
            store.AddProperty(data2);
            store.AddProperty(data3);
            store.AddProperty(data4);
            store.AddProperty(data5);
        }

        private static PropertyStoreTestable GetPropertyStore()
        {
            var provinceStoreMock = new Mock<IProvinceStore>();
            provinceStoreMock
                .Setup(x => x.GetProvinces(It.IsAny<Position>()))
                .Returns(new List<Province>() { new Province() { name = DemoData.ProvinceName } });

            var store = new PropertyStoreTestable(provinceStoreMock.Object, new ArrayMapStrategy());
            return store;
        }
    }
}
