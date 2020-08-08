using System.Collections.Generic;

namespace WordCountProgram
{
    public class ReadFileResult
    {
        public Status Status { get; set; }
        public Dictionary<string, int> WordCounts { get; set; }
        public int TotalWordCount { get; set; }
    }

    public enum Status
    {
        Success = 1,
        InvalidFilePath = 2,
        Error = 3
    }
}
