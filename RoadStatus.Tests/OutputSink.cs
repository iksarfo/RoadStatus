using System.Collections.Generic;

namespace RoadStatus.Tests
{
    public class OutputSink
    {
        private readonly List<string> _messages = new List<string>();

        public void Write(string text)
        {
            _messages.Add(text);
        }

        public IEnumerable<string> Written()
        {
            return _messages;
        }
    }
}