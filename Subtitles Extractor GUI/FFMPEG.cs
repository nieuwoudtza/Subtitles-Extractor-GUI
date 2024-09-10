using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Subtitles_Extractor_GUI
{
    public static class FFMPEG
    {
        public static List<SubtitleStream> GetSubtitleStreams(string input)
        {
            List<SubtitleStream> subtitleStreams = new List<SubtitleStream>();

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = "ffmpeg",
                Arguments = "-i \"" + input + "\"",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
            };
            Process proc = Process.Start(psi);
            string[] output = proc.StandardError.ReadToEnd().ToLower().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            proc.WaitForExit();

            for (int j = 0; j < output.Length; j++)
            {
                string line = output[j].Trim();
                if (line.Contains("): subtitle: subrip") || line.Contains("): subtitle: mov_text") || line.Contains("): subtitle: hdmv_pgs_subtitle"))
                {
                    line = line.Substring("stream #".Length);
                    string stream = line.Substring(0, line.IndexOf("("));
                    string language = line.Substring(line.IndexOf("(") + 1);
                    language = language.Substring(0, language.IndexOf(")"));

                    SubtitleStream subtitleStream = new SubtitleStream()
                    {
                        Input = input,
                        Stream = stream,
                        Language = language,
                    };

                    if (line.Contains("): subtitle: subrip") || line.Contains("): subtitle: mov_text"))
                    {
                        subtitleStream.SubtitleStreamType = SubtitleStreamType.subrip_mov_text;
                    }
                    else
                    {
                        subtitleStream.SubtitleStreamType = SubtitleStreamType.hdmv_pgs_subtitle;
                    }

                    subtitleStreams.Add(subtitleStream);
                }
            }

            return subtitleStreams;
        }
    }
}
