using Newtonsoft.Json;

namespace RoadStatus
{
    public class RoadStatus
    {
        public RoadStatus(/* for deserialization */)
        {
        }

        public RoadStatus(string id) : this()
        {
            Id = id;
        }

        public RoadStatus(
            string id,
            string displayName,
            string severity,
            string description) : this(id)
        {
            DisplayName = displayName;
            StatusSeverity = severity;
            StatusSeverityDescription = description;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; private set; }

        [JsonProperty(PropertyName = "statusSeverity")]
        public string StatusSeverity { get; private set; }

        [JsonProperty(PropertyName = "statusSeverityDescription")]
        public string StatusSeverityDescription { get; private set; }
    }
}