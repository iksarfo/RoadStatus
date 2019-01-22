using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace RoadStatus
{
    public static class RoadStatusResponseParser
    {
        public static RoadStatusResult Parse(string json)
        {
            var statuses = JsonConvert.DeserializeObject<List<RoadStatus>>(json);

            //TODO: ask whether multiple status results constitutes an error!

            var status = statuses.FirstOrDefault()
                         ?? throw new ArgumentOutOfRangeException(nameof(json), json, "no status found");

            return new RoadStatusResult(status);
        }
    }
}