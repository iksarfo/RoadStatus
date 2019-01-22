using System.Configuration;

namespace RoadStatus
{
    public static class AppSettings
    {
        public static readonly string AppId = ConfigurationManager.AppSettings["AppId"];
        public static readonly string Key = ConfigurationManager.AppSettings["Key"];
    }
}