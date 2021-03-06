﻿using System.Linq;
using VivaSpotippos.Model.Entities;
using VivaSpotippos.Stores;
using Xunit;

namespace VivaSpotippos.Test
{
    public class ProvinceStoreTest
    {
        /// <summary>
        /// Given a position without sobreposition of provinces
        /// When gets provinces based on the position
        /// Then it should return the correct province
        /// </summary>
        [Theory]
        [InlineData(0, 0, "Scavy")]
        [InlineData(100, 600, "Gode")]
        [InlineData(700, 100, "Groola")]
        [InlineData(700, 700, "Ruja")]
        [InlineData(1000, 100, "Nova")]
        [InlineData(1200, 1000, "Jaby")]
        public void FindProvinceByPositionWithoutSobreposition(int x, int y, string province)
        {
            // Arrange
            var store = new ProvinceStore();

            var position = new Position(x, y);

            // Act
            var foundProvince = store.GetProvinces(position);

            // Assert
            Assert.NotNull(foundProvince);
            Assert.Equal(1, foundProvince.Count);

            Assert.Equal(province, foundProvince.First().Name);
        }

        /// <summary>
        /// Given a position with sobreposition of provinces
        /// When gets provinces based on the position
        /// Then it should return the correct province list
        /// </summary>
        [Theory]
        [InlineData(500, 700, "Gode", "Ruja")]
        [InlineData(400, 700, "Gode", "Ruja")]
        [InlineData(600, 700, "Gode", "Ruja")]
        [InlineData(600, 500, "Gode", "Ruja", "Scavy", "Groola")]
        [InlineData(800, 500, "Ruja", "Nova", "Groola")]
        public void FindProvinceByPositionWithSobreposition(int x, int y, params string[] provinces)
        {
            // Arrange
            var store = new ProvinceStore();

            var position = new Position(x, y);

            // Act
            var foundProvince = store.GetProvinces(position);

            // Assert
            Assert.NotNull(foundProvince);
            Assert.Equal(provinces.Count(), foundProvince.Count);

            var listOfProvinceNames = foundProvince.Select(p => p.Name);

            foreach (var item in provinces)
            {
                Assert.Contains(item, listOfProvinceNames);
            }
        }
    }
}
