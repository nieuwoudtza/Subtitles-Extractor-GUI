using System.Collections.Generic;

namespace Subtitles_Extractor_GUI
{
    public class MediaFile
    {
        public string Path { get; set; }

        public List<SubtitleStream> SubtitleStreams { get; set; }


        public string PathLower => Path.ToLower();
    }
}
