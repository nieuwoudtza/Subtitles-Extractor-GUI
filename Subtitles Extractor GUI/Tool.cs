using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Subtitles_Extractor_GUI
{
    public static class Tool
    {
        static string _applicationPath = Assembly.GetExecutingAssembly().Location;
        static string _sendTo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\SendTo\Subtitles Extractor.lnk";

        public static void CreateShortcut()
        {
            WshShell wsh = new WshShell();
            IWshShortcut shortcut = wsh.CreateShortcut(_sendTo);
            shortcut.Arguments = "";
            shortcut.TargetPath = _applicationPath;
            shortcut.WindowStyle = 1;
            shortcut.IconLocation = _applicationPath;
            shortcut.Save();
        }

        public static bool ShortcutExists()
        {
            return System.IO.File.Exists(_sendTo);
        }

        public static void RemoveShortcut()
        {
            System.IO.File.Delete(_sendTo);
        }

        public static List<string> SearchDirectory(string dir, bool subdirectories, string[] searchPatterns = null)
        {
            var results = new List<string>();

            try
            {
                // Search for files in the current directory based on each search pattern
                if (searchPatterns != null)
                {
                    foreach (var pattern in searchPatterns)
                    {
                        try
                        {
                            if (!pattern.EndsWith("*"))
                            {
                                // Extract the file extension part from the pattern
                                string fileExtensionPattern = Path.GetExtension(pattern);

                                // Extract the filename part from the pattern
                                string fileNamePattern = pattern.Substring(0, pattern.Length - fileExtensionPattern.Length);

                                // Create a regular expression pattern to match the filename part
                                string fileNameRegexPattern = "^" + Regex.Escape(fileNamePattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";

                                // Create a regular expression pattern to match the file extension part
                                string fileExtensionRegexPattern = "^" + Regex.Escape(fileExtensionPattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";

                                // Filter out files matching both filename and file extension patterns
                                results.AddRange(Directory.GetFiles(dir).Where(file => Regex.IsMatch(Path.GetFileNameWithoutExtension(file), fileNameRegexPattern) && Regex.IsMatch(Path.GetExtension(file), fileExtensionRegexPattern)));
                            }
                            else
                            {
                                results.AddRange(Directory.GetFiles(dir, pattern));
                            }
                        }
                        catch
                        {
                            // Ignore exceptions and continue with the next pattern
                        }
                    }
                }
                else
                {
                    results.AddRange(Directory.GetFiles(dir));
                }

                // If subdirectories are enabled, recursively search subdirectories
                if (subdirectories)
                {
                    var subDirs = Directory.GetDirectories(dir);
                    foreach (var subDir in subDirs)
                    {
                        try
                        {
                            results.AddRange(SearchDirectory(subDir, true, searchPatterns));
                        }
                        catch
                        {
                            // Ignore exceptions and continue with the next subdirectory
                        }
                    }
                }
            }
            catch
            {
                // Ignore exceptions and continue with the next directory
            }

            return results;
        }
    }
}
