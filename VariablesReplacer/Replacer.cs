using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EsseivaN.Tools
{
    public class Replacer
    {
        public static List<Parameters> Params { get; set; } = new List<Parameters>();
        private static string OutputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VariablesReplacer");

        public static void ImportFile(string FileName)
        {
            if (!File.Exists(FileName))
            {
                Console.WriteLine("Config file not found");
                return;
            }

            // Read file and remove \r caracters
            string content = File.ReadAllText(FileName).Replace("\r", "");

            // Set the end caracter as 0x1e
            content = content.Replace("###END PARAMETER###\n", ((char)0x1e).ToString());
            string[] importedParams = content.Split((char)0x1e).Where(x => (x != string.Empty) && (x.Contains("###BEGIN PARAMETER###\n"))).ToArray();

            if (importedParams.Length != 0)
            {
                bool IsValid = true;
                foreach (string importedParm in importedParams)
                {
                    Parameters newParameters = new Parameters();
                    string[] Values = importedParm.Replace("###BEGIN PARAMETER###\n", string.Empty).Replace("\n", ((char)0x1e).ToString()).Split((char)0x1e).Where(x => x != String.Empty).ToArray();

                    short HtmlCount = 0;
                    IsValid = true;
                    foreach (string Value in Values)
                    {
                        var temp = Value.Split(new char[] { '#' }, 2);
                        if (temp.Length != 2)
                        {
                            IsValid = false;
                            break;
                        }

                        string index = temp[0];
                        string val = temp[1];

                        if (index == "0")
                        {
                            newParameters.OutputDir = val;
                        }
                        else if (index == "1")
                        {
                            newParameters.Vars.Add(val);
                        }
                        else if (index == "2")
                        {
                            newParameters.HtmlFiles.Add(val);
                            newParameters.HtmlFilesOutput.Add(string.Empty);
                            HtmlCount++;
                        }
                        else if (index == "3")
                        {
                            newParameters.ContentFile.Add(val);
                        }
                        else if (index == "4")
                        {
                            newParameters.VarInFile = val.ToUpper() == bool.TrueString.ToUpper();
                        }
                        else if (index == "5")
                        {
                            newParameters.HtmlFilesOutput[(HtmlCount - 1)] = val;
                        }
                    }
                    if (IsValid)
                    {
                        Params.Add(newParameters);
                    }
                }
                Console.WriteLine(Params.Count + " imported");
            }
            else
            {
                Console.WriteLine("Invalid config file");
            }
        }

        public static void ExecuteScripts(Parameters parameter)
        {
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }

            // Delete old file
            foreach (string htmlFile in parameter.HtmlFiles)
            {
                File.Delete(Path.Combine(OutputPath, Path.GetFileName(htmlFile)));
            }
            for (short i = 0; i < parameter.HtmlFiles.Count; i++)
            {
                string htmlFile = parameter.HtmlFiles[i];
                string htmlPath = Path.Combine(OutputPath, Path.GetFileName(htmlFile));

                // Read base text or already changed text
                string html = File.Exists(htmlPath) ? File.ReadAllText(htmlPath) : File.ReadAllText(htmlFile);

                foreach (string contentfile in parameter.ContentFile)
                {
                    string content = File.ReadAllText(contentfile);

                    if (parameter.VarInFile)
                    {
                        string[] variablesContent = content.Replace("###END VARIABLE###\r\n", ((char)0x1e).ToString()).Replace("###END VARIABLE###", string.Empty).Split((char)0x1e).Where(x => (x != string.Empty) && (x.Contains("###BEGIN VARIABLE###\r\n"))).ToArray();
                        Console.WriteLine(variablesContent.Length + " variables found in " + contentfile);
                        foreach (string variableContent in variablesContent)
                        {
                            var temp = variableContent.Replace("###BEGIN VARIABLE###\r\n", string.Empty).Split(new string[] { "#\r\n" }, 2, StringSplitOptions.RemoveEmptyEntries);
                            if (temp.Length != 2)
                            {
                                break;
                            }

                            string name = $"{{{temp[0]}}}";
                            if (parameter.Vars.Contains(name))
                            {
                                Console.WriteLine("found variable " + name + " in " + contentfile);
                                string text = temp[1];
                                html = html.Replace(name, text);
                            }
                            else
                            {
                                Console.WriteLine("No variable found for " + name + " in " + contentfile);
                            }
                        }
                    }
                    else
                    {
                        foreach (string variable in parameter.Vars)
                        {
                            html = html.Replace(variable, content);
                        }
                    }

                    string path = (parameter.HtmlFilesOutput[i] == string.Empty) ?
                        Path.Combine(Path.GetFullPath(parameter.OutputDir), Path.GetFileName(htmlFile)) :
                        parameter.HtmlFilesOutput[i];

                    File.WriteAllText(path, html);
                }
                Console.WriteLine("Replacement for " + htmlFile);
            }
        }


        public class TestSettings
        {
            public string[] DataFiles { get; set; }
            public string[] OutputFiles { get; set; }
            public string[] ContentFiles { get; set; }
            public string[] Variables { get; set; }
            public bool VarInFiles { get; set; }
            public string OutputPath { get; set; }
        }

        public static void TestImport(string path)
        {
            SettingsManager<TestSettings> settingsManager = new SettingsManager<TestSettings>();
            var t = settingsManager.Load(path);
        }

        public static void TestExport(Parameters[] parameters)
        {
            SettingsManager<TestSettings> settingsManager = new SettingsManager<TestSettings>();
            foreach (Parameters param in parameters)
            {
                settingsManager.AddSetting(new TestSettings()
                {
                    DataFiles = param.HtmlFiles.ToArray(),
                    OutputFiles = param.HtmlFilesOutput.ToArray(),
                    ContentFiles = param.ContentFile.ToArray(),
                    Variables = param.Vars.ToArray(),
                    VarInFiles = param.VarInFile,
                    OutputPath = param.OutputDir,
                });
            }
            settingsManager.Save(Path.Combine(Environment.CurrentDirectory, "Debug.cfg"));
        }

        public static void ExecuteScript()
        {
            Console.WriteLine(Params.Count + " replacement to do");
            foreach (var parameter in Params)
            {
                ExecuteScripts(parameter);
            }
        }

        public class Parameters
        {
            public List<string> Vars { get; set; } = new List<string>();
            public List<string> HtmlFiles { get; set; } = new List<string>();
            public List<string> HtmlFilesOutput { get; set; } = new List<string>();
            public List<string> ContentFile { get; set; } = new List<string>();
            public bool VarInFile { get; set; }
            public string OutputDir { get; set; }

            public Parameters()
            {

            }

            public override string ToString()
            {
                return $"Replace {string.Join(";", Vars)} in {string.Join(" ; ", HtmlFiles)} {(VarInFile ? "by variables in" : "by content of")} {string.Join(" ; ", ContentFile)} - Output to {OutputDir}";
            }
        }

    }
}
