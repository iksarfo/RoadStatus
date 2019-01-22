using System;

namespace RoadStatus
{
    public static class OutputFormatter
    {
        public static string FormatSuccess(RoadStatusResult result)
        {
            return $"The status of the {result.Road.DisplayName} is as follows"
                   + Environment.NewLine + $"\tRoad Status is {result.Road.StatusSeverity}"
                   + Environment.NewLine + $"\tRoad Status Description is {result.Road.StatusSeverityDescription}";
        }

        public static string FormatFailure(RoadStatusResult result)
        {
            return result.Status == ExitCodes.InvalidRoad
                ? $"{result.Road.Id} is not a valid road"
                : "An unexpected error occurred";
        }
    }
}