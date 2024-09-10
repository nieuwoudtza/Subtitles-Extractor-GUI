using System.Diagnostics;
using System.IO;
using System;
using System.Linq;

namespace Subtitles_Extractor_GUI
{
    public class SubtitleStream
    {
        public string Input { get; set; }
        public string Stream { get; set; }
        public string Language { get; set; }
        public SubtitleStreamType SubtitleStreamType { get; set; }

        //public bool ExtractSuccess { get; set; }
        //public string ErrorMessage { get; set; }

        public void Extract()
        {
            string subtitleFilePath = Path.ChangeExtension(Input, ".srt");
            if (File.Exists(subtitleFilePath))
            {
                try
                {
                    File.Delete(subtitleFilePath);
                }
                catch { }
            }

            switch (SubtitleStreamType)
            {
                case SubtitleStreamType.subrip_mov_text:
                    ExtractSubripMovText();
                    break;
                case SubtitleStreamType.hdmv_pgs_subtitle:
                    ExtractHdmvPgs();
                    break;
            }
        }

        void ExtractSubripMovText()
        {
            ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", "-i \"" + Input + "\"" + " -map " + Stream + " \"" + Path.ChangeExtension(Input, ".srt") + "\" -y")
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true
            };

            Process extract = new Process
            {
                StartInfo = psi
            };
            Info.RunningProcesses.Add(extract);
            extract.Start();
            string error = GetError(extract.StandardError.ReadToEnd()).Trim();
            extract.WaitForExit();
            Info.RunningProcesses.Remove(extract);

            if (error.StartsWith("video:0kB audio:0kB subtitle:") && error.EndsWith("%"))
            {
                error = string.Empty;
            }
            else if (error.StartsWith("size="))
            {
                error = string.Empty;
            }

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
        }

        void ExtractHdmvPgs()
        {
            string supFile = Path.ChangeExtension(Input, ".sup");

            ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", "-i \"" + Input + "\" -map " + Stream + " -c:s copy \"" + supFile + "\" -y")
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true
            };

            Process extract = new Process
            {
                StartInfo = psi
            };
            Info.RunningProcesses.Add(extract);
            extract.Start();
            string error = GetError(extract.StandardError.ReadToEnd());
            extract.WaitForExit();
            Info.RunningProcesses.Remove(extract);

            FileInfo fi = new FileInfo(supFile);
            if (!fi.Exists || (!string.IsNullOrEmpty(error) && fi.Length == 0))
            {
                throw new Exception("Unable to extract .sup file");
            }
            else if (error.StartsWith("size="))
            {
                error = string.Empty;
            }

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
        }

        public void PerformOCR()
        {
            string supFile = Path.ChangeExtension(Input, ".sup");

            //string SubEditPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SE361", "SubtitleEdit.exe");
            string SubEditPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SE", "SubtitleEdit.exe");
            //File.WriteAllText(TempFile, "\"" + SubEditPath + "\" /convert \"" + supFile + "\"" + " SubRip /fps:25");

            ProcessStartInfo psi = new ProcessStartInfo(SubEditPath)
            {
                Arguments = "/convert \"" + supFile + "\"" + " SubRip /fps:25",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true
            };

            Process ocr = new Process
            {
                StartInfo = psi
            };

            Info.RunningProcesses.Add(ocr);

            ocr.Start();
            string error = GetError(ocr.StandardError.ReadToEnd());
            ocr.WaitForExit();

            Info.RunningProcesses.Remove(ocr);

            try
            {
                if (File.Exists(supFile))
                {
                    File.Delete(supFile);
                }
            }
            catch { }

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
        }

        string GetError(string Output)
        {
            if (Output.Length > 0)
            {
                string[] error = Output.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                return error.Last();
            }

            return "";
        }
    }

    public enum SubtitleStreamType
    {
        subrip_mov_text,
        hdmv_pgs_subtitle
    }
}
