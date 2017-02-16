using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using VivaSpotippos.Model;
using VivaSpotippos.Model.Entities;
using VivaSpotippos.Model.Mapping;
using VivaSpotippos.Test;
using Xunit;

namespace VivaSpotippos.Performance
{
    public class TestInfo
    {
        public IMapStrategy MapStrategy { get; set; }
        public Func<int, int, bool> FillMap { get; set; }
        public int SizeFactor { get; set; }
    }

    public class MappingPerformance
    {
        public static IEnumerable<object[]> MappingStrategyTest()
        {
            yield return new object[] {
                new TestInfo() { MapStrategy = new ArrayMapStrategy(), FillMap = LowDensityDistribution, SizeFactor = 1 } };
            yield return new object[] {
                new TestInfo() { MapStrategy = new ArrayMapStrategy(), FillMap = LowDensityDistribution, SizeFactor = 2 } };
            yield return new object[] {
                new TestInfo() { MapStrategy = new ArrayMapStrategy(), FillMap = LowDensityDistribution, SizeFactor = 10 } };

            yield return new object[] {
                new TestInfo() { MapStrategy = new ArrayMapStrategy(), FillMap = HighDensityDistribution, SizeFactor = 1 } };
            yield return new object[] {
                new TestInfo() { MapStrategy = new ArrayMapStrategy(), FillMap = HighDensityDistribution, SizeFactor = 2 } };
            yield return new object[] {
                new TestInfo() { MapStrategy = new ArrayMapStrategy(), FillMap = HighDensityDistribution, SizeFactor = 10 } };
        }

        [Theory]
        [MemberData(nameof(MappingStrategyTest))]
        public void CheckPerformance(TestInfo testInfo)
        {
            var mapStrategy = testInfo.MapStrategy;
            var sizeFactor = testInfo.SizeFactor;

            var elapsedTimeSet = new List<int>();

            // Arrange
            mapStrategy.ResetMap();

            AddPropertiesToMap(mapStrategy, testInfo.FillMap);

            // Act 
            for (int i = 0; i < 100; i++)
            {
                var stopWatch = Stopwatch.StartNew();

                mapStrategy.GetOnMap(
                    new Position(VivaSettings.MinMapX, VivaSettings.MinMapY),
                    new Position(VivaSettings.MaxMapX / sizeFactor - 1, VivaSettings.MaxMapY / sizeFactor - 1));

                stopWatch.Stop();

                elapsedTimeSet.Add(stopWatch.Elapsed.Milliseconds);
            }

            // Assert
            AppendResultToTxt(mapStrategy, testInfo.FillMap, sizeFactor, elapsedTimeSet);
        }

        private static void AppendResultToTxt(IMapStrategy mapStrategy, Func<int, int, bool> addPropertyStrategy, int sizeFactor, List<int> elapsedTimeSet)
        {
            var result = new StringBuilder();

            float average = (float)elapsedTimeSet.Sum() / elapsedTimeSet.Count;
            float desvio = elapsedTimeSet.Select(x => Math.Abs(x - average)).Sum() / elapsedTimeSet.Count;

            result.AppendLine(string.Format("Test (ms): {0} - {1} - Fator de redução:{2}",
                mapStrategy.GetType().Name,
                addPropertyStrategy.GetMethodInfo().Name,
                sizeFactor));

            result.AppendLine(string.Format("Avg: {0:N5}", average));
            result.AppendLine(string.Format("Dsv: {0:N5}", desvio));

            File.AppendAllText("PerformanceResult.txt", result.ToString());
        }

        public static bool LowDensityDistribution(int x, int y)
        {
            return x % 28 == 0 && y % 20 == 0;
        }

        public static bool HighDensityDistribution(int x, int y)
        {
            return x % 2 == 0 && y % 2 == 0;
        }

        private static void AddPropertiesToMap(IMapStrategy mapStrategy, Func<int, int, bool> shouldAddProperty)
        {
            int memoryIdentity = 1;

            for (int xPosition = VivaSettings.MinMapX; xPosition < VivaSettings.MaxMapX; xPosition++)
            {
                for (int yPosition = VivaSettings.MinMapY; yPosition < VivaSettings.MaxMapY; yPosition++)
                {
                    if (shouldAddProperty(xPosition, yPosition))
                    {
                        var property = Property.CreateFrom(DemoData.ValidPostRequest);
                        property.X = xPosition;
                        property.Y = yPosition;
                        property.Description = string.Format("Position: {0:D4}{1:D4}", xPosition, yPosition);
                        property.Id = memoryIdentity++;

                        mapStrategy.AddToMap(property);
                    }
                }
            }
        }
    }
}
