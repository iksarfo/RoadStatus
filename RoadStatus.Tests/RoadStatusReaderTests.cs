using System;
using System.Net;
using Xunit;

namespace RoadStatus.Tests
{
    public class RoadStatusReaderTests
    {
        [Fact]
        public void Road_not_found_throws_handled_web_exception()
        {
            const string fakeUrl = "https://api.tfl.gov.uk/fake";
            const string fakeRoadId = "fakeRoadId";
            const string fakeErrorMessage = "fake error message";

            string FakeResourceReader(Uri _) => throw new WebException(fakeErrorMessage);
            HttpStatusCode? GetHttpStatusCode(WebException _) => HttpStatusCode.NotFound;
            RoadStatusReader.WebExceptionToHttpStatusCode = GetHttpStatusCode;
            
            var reader =
                new RoadStatusReader(fakeUrl)
                    .GetWith(FakeResourceReader);

            var result = reader.GetResult(fakeRoadId);

            Assert.Equal(ExitCodes.InvalidRoad, result.Status);
            Assert.Equal(fakeErrorMessage, result.ErrorMessage);
            Assert.Equal(fakeRoadId, result.Road.Id);
        }

        [Fact]
        public void Random_error_throws_handled_web_exception()
        {
            const string fakeUrl = "https://api.tfl.gov.uk/fake";
            const string fakeRoadId = "fakeRoadId";
            const string fakeErrorMessage = "fake error message";

            string FakeResourceReader(Uri _) => throw new WebException(fakeErrorMessage);
            HttpStatusCode? GetHttpStatusCode(WebException _) => HttpStatusCode.Forbidden;
            RoadStatusReader.WebExceptionToHttpStatusCode = GetHttpStatusCode;
            
            var reader =
                new RoadStatusReader(fakeUrl)
                    .GetWith(FakeResourceReader);

            var result = reader.GetResult(fakeRoadId);

            Assert.Equal(ExitCodes.Unexpected, result.Status);
            Assert.Equal(fakeErrorMessage, result.ErrorMessage);
            Assert.Null(result.Road?.Id);
        }

        [Fact]
        public void Random_error_throws_handled_exception()
        {
            const string fakeUrl = "https://api.tfl.gov.uk/fake";
            const string fakeRoadId = "fakeRoadId";
            const string fakeErrorMessage = "fake error message";

            string FakeResourceReader(Uri _) => throw new Exception(fakeErrorMessage);
            
            var reader =
                new RoadStatusReader(fakeUrl)
                    .GetWith(FakeResourceReader);

            var result = reader.GetResult(fakeRoadId);

            Assert.Equal(ExitCodes.Unexpected, result.Status);
            Assert.Equal(fakeErrorMessage, result.ErrorMessage);
            Assert.Null(result.Road?.Id);
        }

        [Fact]
        public void Default_parser_is_replaced()
        {
            const string fakeUrl = "https://api.tfl.gov.uk/fake";
            const string fakeJsonResponse = "fake json response";
            const string fakeRoadId = "fakeRoadId";
            const string fakeDisplayName = "fake display name";
            const string fakeSeverity = "fake severity";
            const string fakeDescription = "fake description";
            
            string FakeResourceReader(Uri _) => fakeJsonResponse;

            var fakeRoadStatus = new RoadStatus(fakeRoadId, fakeDisplayName, fakeSeverity, fakeDescription);
            RoadStatusResult FakeParser(string _) => new RoadStatusResult(fakeRoadStatus);
            
            var reader =
                new RoadStatusReader(fakeUrl)
                    .ParseWith(FakeParser)
                    .GetWith(FakeResourceReader);

            var result = reader.GetResult(fakeRoadId);

            Assert.Equal(ExitCodes.Success, result.Status);
            Assert.Equal(fakeRoadId, result.Road.Id);
            Assert.Equal(fakeDisplayName, result.Road.DisplayName);
            Assert.Equal(fakeSeverity, result.Road.StatusSeverity);
            Assert.Equal(fakeDescription, result.Road.StatusSeverityDescription);
        }
    }
}