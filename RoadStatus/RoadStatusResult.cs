using System;
using System.Net;

namespace RoadStatus
{
    public class RoadStatusResult
    {
        public RoadStatusResult(RoadStatus road)
        {
            Road = road ?? throw new ArgumentNullException(nameof(road));
            Status = ExitCodes.Success; //TODO: ask about using OK property below to determine "Success"
        }

        public RoadStatusResult(
            WebException exception,
            string roadId)
            : this(new RoadStatus(roadId))
        {
            ErrorMessage = exception?.Message ?? throw new ArgumentNullException(nameof(exception));
            Status = ExitCodes.InvalidRoad;
        }

        public RoadStatusResult(Exception exception)
        {
            ErrorMessage = exception?.Message ?? throw new ArgumentNullException(nameof(exception));
            Status = ExitCodes.Unexpected;
        }

        public RoadStatus Road { get; }
        
        public ExitCodes Status { get; }

        public string ErrorMessage { get; }

        //TODO: ask whether the completeness of the road status should matter (see 1st ctor)
        public bool OK => !string.IsNullOrWhiteSpace(Road.DisplayName) &&
                          !string.IsNullOrWhiteSpace(Road.StatusSeverity) &&
                          !string.IsNullOrWhiteSpace(Road.StatusSeverityDescription);
    }
}