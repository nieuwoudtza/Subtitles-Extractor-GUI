using System.Collections.Generic;
using System.Diagnostics;

namespace Subtitles_Extractor_GUI
{
    public static class Info
    {
        public static List<Process> RunningProcesses = new List<Process>();

        public static string[] ValidFileExtensions = new string[]
        {
            ".avi",
            ".mkv",
            ".mov",
            ".mp4",
            ".webm",
        };
    }
}
