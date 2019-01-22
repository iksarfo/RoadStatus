using System;
using System.Collections.Generic;
using System.Linq;

namespace RoadStatus
{
    public static class Program
    {
        public static Action<string> Respond = ConsoleWriter.WriteLine;

        private static readonly Dictionary<ExitCodes, Func<RoadStatusResult, string>> GetResponse
            = new Dictionary<ExitCodes, Func<RoadStatusResult, string>>
            {
                {ExitCodes.Success, OutputFormatter.FormatSuccess},
                {ExitCodes.InvalidRoad, OutputFormatter.FormatFailure},
                {ExitCodes.Unexpected, OutputFormatter.FormatFailure}
            };

        public static void Main(string[] args)
        {
            if (!args.Any())
            {
                Respond("Missing <road Id> is required");
                Environment.ExitCode = (int) ExitCodes.MissingArgument;
                return;
            }

            var result = new RoadStatusReader(Url.Status).GetResult(args.First());

            Respond(GetResponse[result.Status](result));

            Environment.ExitCode = (int) result.Status;
        }
    }
}