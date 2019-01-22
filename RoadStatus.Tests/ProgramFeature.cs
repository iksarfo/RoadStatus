using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xbehave;
using Xunit;

namespace RoadStatus.Tests
{
    public class ProgramFeature
    {
        [Scenario]
        public void MissingArguments(OutputSink outputSink, string[] args)
        {
            "Given no arguments are supplied"
                .x(() => args = new string[]{});

            "And output is being intercepted"
                .x(() =>
                {
                    outputSink = new OutputSink();
                    Program.Respond = outputSink.Write;
                });

            "When the program is executed"
                .x(() => Program.Main(args));

            "Then the output indicates missing argument"
                .x(() => Assert.Contains("Missing <road Id> is required", outputSink.Written()));

            "And the exit code is 'Missing Argument'"
                .x(() => Assert.Equal((int) ExitCodes.MissingArgument, Environment.ExitCode));
        }

        [Scenario]
        public void NonExistentRoad(OutputSink outputSink, string[] args)
        {
            "Given an invalid road Id is supplied"
                .x(() => args = new[]{ "X0" });

            "And output is being intercepted"
                .x(() =>
                {
                    outputSink = new OutputSink();
                    Program.Respond = outputSink.Write;
                });

            "When the program is executed"
                .x(() => Program.Main(args));

            "Then the output indicates invalid road Id"
                .x(() => Assert.Contains("X0 is not a valid road", outputSink.Written()));

            "And the exit code is 'Invalid Road'"
                .x(() => Assert.Equal((int) ExitCodes.InvalidRoad, Environment.ExitCode));
        }

        [Scenario]
        public void ValidRoad(OutputSink outputSink, string[] args)
        {
            "Given an valid road Id is supplied"
                .x(() => args = new[]{ "A2" });

            "And output is being intercepted"
                .x(() =>
                {
                    outputSink = new OutputSink();
                    Program.Respond = outputSink.Write;
                });

            "When the program is executed"
                .x(() => Program.Main(args));

            "Then the output includes the display name"
                .x(() =>
                {
                    Assert.Matches(
                        new Regex("The status of the .+ is as follows"),
                        outputSink.Written().Single());
                });

            "And the output includes the road status"
                .x(() =>
                {
                    Assert.Matches(
                        new Regex("Road Status is .+"),
                        outputSink.Written().Single());
                });

            "And the output includes the road status description"
                .x(() =>
                {
                    Assert.Matches(
                        new Regex("Road Status Description is .+"),
                        outputSink.Written().Single());
                });

            "And the exit code is 'Success'"
                .x(() => Assert.Equal(
                    (int) ExitCodes.Success,
                    Environment.ExitCode));
        }
    }
}