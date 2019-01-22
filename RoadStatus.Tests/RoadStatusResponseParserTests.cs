using System;
using Xunit;

namespace RoadStatus
{
    public class RoadStatusResponseParserTests
    {
        [Fact]
        public void Missing_road_statuses_throws_exception()
        {
            const string missingStatuses = "[]";
            Assert.Throws<ArgumentOutOfRangeException>(() => RoadStatusResponseParser.Parse(missingStatuses));
        }

        [Fact]
        public void Parses_road_status()
        {
            const string validJsonResponse =
                @"[{
                    ""id"": ""a2"",
                    ""displayName"": ""A2"",
                    ""statusSeverity"": ""Good"",
                    ""statusSeverityDescription"": ""No Exceptional Delays""
                }]";

            var road = RoadStatusResponseParser.Parse(validJsonResponse).Road;

            Assert.Equal("A2", road.DisplayName);
            Assert.Equal("Good", road.StatusSeverity);
            Assert.Equal("No Exceptional Delays", road.StatusSeverityDescription);
        }
    }
}