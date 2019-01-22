using System;
using System.IO;
using System.Net;

namespace RoadStatus
{
    public static class HttpWebResponseReader
    {
        // ReSharper disable once AssignNullToNotNullAttribute
        public static string Read(Uri uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            using(var response = (HttpWebResponse)request.GetResponse())
            using(var stream = response.GetResponseStream())
            using(var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}