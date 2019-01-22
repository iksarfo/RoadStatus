using System.Net;

namespace RoadStatus
{
    internal static class HttpStatusCodeReader
    {
        public static HttpStatusCode? GetHttpStatusCode(WebException wex)
        {
            var errorResponse = wex.Response as HttpWebResponse;
            return errorResponse?.StatusCode;
        }
    }
}