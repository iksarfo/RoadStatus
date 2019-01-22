namespace RoadStatus
{
    public static class Url
    {
        public static readonly string Status = $"https://api.tfl.gov.uk/Road/{{0}}?app_id={AppSettings.AppId}&app_key={AppSettings.Key}";
    }
}