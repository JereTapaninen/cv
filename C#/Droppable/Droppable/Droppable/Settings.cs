using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Droppable
{
    public class Settings
    {
        private readonly string SETTINGS_FILE = Application.StartupPath + @"\Settings.ini";

        /// <summary>
        /// This option, when turned on, filters all songs that have the word "cover" in them
        /// </summary>
        public bool FilterCovers = true;
        /// <summary>
        /// This option, when turned on, filters all songs that have the word "remix" in them
        /// </summary>
        public bool FilterRemixes = true;
        public bool FilterNightcore = true;
        /// <summary>
        /// This option, when turned on, 
        /// filters all the songs that do not have the song name and also the artist name in the download URL
        /// </summary>
        public bool ExactMatch = false;
        public bool PartialMatch = true;
        /// <summary>
        /// Searches faster but doesn't check all the possible results
        /// </summary>
        public bool FastSearch = true;
        public int MinimumSizeKB = 200;
        public int TimeoutSeconds = 60;

        public bool SkipEngineCheck = false;

        public Settings()
        { }

        public Settings(bool filterCovers, bool filterRemixes, bool filterNightcore, bool exactMatch, bool partialMatch,
            bool fastSearch, int minimumSizeKB, int timeoutSeconds, bool skipEngineCheck)
        {
            FilterCovers = filterCovers;
            FilterRemixes = filterRemixes;
            FilterNightcore = filterNightcore;
            ExactMatch = exactMatch;
            PartialMatch = partialMatch;
            FastSearch = fastSearch;
            MinimumSizeKB = minimumSizeKB;
            TimeoutSeconds = timeoutSeconds;
            SkipEngineCheck = skipEngineCheck;
        }

        public void Load()
        {
            if (!File.Exists(SETTINGS_FILE))
            {
                Create();
            }
            else
            {
                using (var settingsFile = File.OpenRead(SETTINGS_FILE))
                {
                    using (var reader = new StreamReader(SETTINGS_FILE))
                    {
                        var line = string.Empty;

                        while (!string.IsNullOrEmpty(line = reader.ReadLine()) 
                            || reader.Peek() > 0)
                        {
                            if (!line.StartsWith("#") && line.Contains("="))
                            {
                                var keyValPair = new KeyValuePair<string, string>(line.Split('=')[0], line.Split('=')[1]);

                                if (keyValPair.Key == "FilterCovers")
                                {
                                    FilterCovers = bool.Parse(keyValPair.Value);
                                }
                                else if (keyValPair.Key == "FilterRemixes")
                                {
                                    FilterRemixes = bool.Parse(keyValPair.Value);
                                }
                                else if (keyValPair.Key == "FilterNightcore")
                                {
                                    FilterNightcore = bool.Parse(keyValPair.Value);
                                }
                                else if (keyValPair.Key == "ExactMatch")
                                {
                                    ExactMatch = bool.Parse(keyValPair.Value);

                                    if (ExactMatch)
                                        PartialMatch = !bool.Parse(keyValPair.Value);
                                }
                                else if (keyValPair.Key == "PartialMatch")
                                {
                                    PartialMatch = bool.Parse(keyValPair.Value);

                                    if (PartialMatch)
                                        ExactMatch = !bool.Parse(keyValPair.Value);
                                }
                                else if (keyValPair.Key == "FastSearch")
                                {
                                    FastSearch = bool.Parse(keyValPair.Value);
                                }
                                else if (keyValPair.Key == "MinimumSizeKB")
                                {
                                    MinimumSizeKB = int.Parse(keyValPair.Value);
                                }
                                else if (keyValPair.Key == "SkipEngineCheck")
                                {
                                    SkipEngineCheck = bool.Parse(keyValPair.Value);
                                }
                                else if (keyValPair.Key == "TimeoutSeconds")
                                {
                                    TimeoutSeconds = int.Parse(keyValPair.Value);
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Settings loaded");
        }

        public void Save()
        {
            if (!File.Exists(SETTINGS_FILE))
            {
                Create();
            }
            else
            {
                var lines = File.ReadAllLines(SETTINGS_FILE);

                for (var i = 0; i < lines.Length; i++)
                {
                    if (!lines[i].StartsWith("#"))
                    {
                        if (lines[i].Contains("="))
                        {
                            var keyValPair = new string[] { lines[i].Split('=')[0], lines[i].Split('=')[1] };

                            if (keyValPair[0] == "FilterCovers")
                                keyValPair[1] = FilterCovers.ToString();
                            else if (keyValPair[0] == "FilterRemixes")
                                keyValPair[1] = FilterRemixes.ToString();
                            else if (keyValPair[0] == "FilterNightcore")
                                keyValPair[1] = FilterNightcore.ToString();
                            else if (keyValPair[0] == "ExactMatch")
                                keyValPair[1] = ExactMatch.ToString();
                            else if (keyValPair[0] == "PartialMatch")
                                keyValPair[1] = PartialMatch.ToString();
                            else if (keyValPair[0] == "FastSearch")
                                keyValPair[1] = FastSearch.ToString();
                            else if (keyValPair[0] == "MinimumSizeKB")
                                keyValPair[1] = MinimumSizeKB.ToString();
                            else if (keyValPair[0] == "SkipEngineCheck")
                                keyValPair[1] = SkipEngineCheck.ToString();
                            else if (keyValPair[0] == "TimeoutSeconds")
                                keyValPair[1] = TimeoutSeconds.ToString();

                            lines[i] = string.Format("{0}={1}", keyValPair);
                        }
                    }
                }

                File.WriteAllLines(SETTINGS_FILE, lines);

                Console.WriteLine("Settings saved!");
            }
        }

        public void Create()
        {
            var fInfos = typeof(Settings).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var fDictionary = new Dictionary<string, string>();

            foreach (var f in fInfos)
                fDictionary.Add(f.Name, f.GetValue(this).ToString());

            var settingsFile = new string[]
            {
                "# Droppable Settings File",
                "# (C) Jere Tapaninen 2016",
                "",
                "# Filters all songs that have the word \"cover\" in the download URL",
                string.Format("FilterCovers={0}", fDictionary["FilterCovers"]),
                "# Filters all songs that have the word \"remix\" in the download URL",
                string.Format("FilterRemixes={0}", fDictionary["FilterRemixes"]),
                "# Filters all songs that have the word \"nightcore\" in the download URL",
                string.Format("FilterNightcore={0}", fDictionary["FilterNightcore"]),
                "",
                "# Please keep in mind that only one of these can be set to TRUE",
                "# Only songs that have both the artist name and the song name in the URL are allowed if set to TRUE",
                string.Format("ExactMatch={0}", fDictionary["ExactMatch"]),
                "# Only songs that have the song name in the URL are allowed if set to TRUE",
                string.Format("PartialMatch={0}", fDictionary["PartialMatch"]),
                "",
                "# If set to true, Droppable will search faster but doesn't check all the possible results",
                string.Format("FastSearch={0}", fDictionary["FastSearch"]),
                "",
                "# Files under this KB size will be filtered",
                string.Format("MinimumSizeKB={0}", fDictionary["MinimumSizeKB"]),
                "",
                string.Format("TimeoutSeconds={0}", fDictionary["TimeoutSeconds"]),
                "",
                "# If set to true, Droppable will always skip the engine checking procedure",
                string.Format("SkipEngineCheck={0}", fDictionary["SkipEngineCheck"]),
            };

            File.WriteAllLines(SETTINGS_FILE, settingsFile);

            Console.WriteLine("Settings created!");
        }

        public override string ToString()
        {
            var sbSettings = new StringBuilder();

            sbSettings.AppendLine("# Settings Are As Follows #");

            var fInfo = typeof(Settings).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var f in fInfo)
                sbSettings.AppendLine(string.Format("{0}:{1}", f.Name, f.GetValue(this)));

            return sbSettings.ToString();
        }
    }
}
