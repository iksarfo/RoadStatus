using System;
using System.Net;

namespace RoadStatus
{
    internal class RoadStatusReader
    {
        internal static Func<WebException, HttpStatusCode?> WebExceptionToHttpStatusCode = HttpStatusCodeReader.GetHttpStatusCode;

        private readonly string _url;
        private Func<string, RoadStatusResult> _jsonParser = RoadStatusResponseParser.Parse;
        private Func<Uri, string> _httpGet = HttpWebResponseReader.Read;

        public RoadStatusReader(string url)
        {
            _url = url;
        }

        public RoadStatusReader ParseWith(Func<string, RoadStatusResult> parser)
        {
            _jsonParser = parser ?? throw new ArgumentNullException(nameof(parser), "Null parser");

            return this;
        }

        public RoadStatusReader GetWith(Func<Uri, string> reader)
        {
            _httpGet = reader ?? throw new ArgumentNullException(nameof(reader), "Null reader");

            return this;
        }

        public RoadStatusResult GetResult(string roadId)
        {
            try
            {
                var response = _httpGet(GetUrl(roadId));

                return _jsonParser(response);
            }
            catch (WebException wex)
            {
                //TODO: Log exception!
                return WebExceptionToHttpStatusCode(wex) == HttpStatusCode.NotFound
                    ? new RoadStatusResult(wex, roadId)
                    : new RoadStatusResult(wex);
            }
            catch (Exception ex)
            {
                //TODO: Log exception!
                return new RoadStatusResult(ex);
            }
        }

        private Uri GetUrl(string roadId) =>
            new Uri(_url.Replace("{0}", roadId));
    }
}