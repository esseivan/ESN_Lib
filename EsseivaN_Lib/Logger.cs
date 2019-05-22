using System;
using System.IO;
using System.Text;

namespace EsseivaN.Tools
{
    /// <summary>
    /// Manage logging
    /// </summary>
    public class Logger<T> : Logger
    {
        /// <summary>
        /// The output stream if WriteMode is WriteMode.Stream
        /// </summary>
        public StreamLogger<T> LogToFile_OutputStream { get; set; } = null;

        public Logger()
        {
            creationTime = DateTime.Now;
        }

        protected override string CheckFile(string path)
        {
            if (LogToFile_WriteMode == WriteMode.Stream)
                return "true";

            // Create directory if not existing
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            // If no extension, set .log
            if (!Path.HasExtension(path))
            {
                path = Path.ChangeExtension(path, "log");
            }

            // If file not existing, create
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            // Else if file existing and writemode set to write, clear file
            else
            {
                if (LogToFile_WriteMode == WriteMode.Write)
                {
                    File.WriteAllText(path, string.Empty);
                }
            }

            return path;
        }

        /// <summary>
        /// Write log with custom text
        /// </summary>
        public override void WriteLog(string data, string log_level)
        {
            if (!Enabled)
                return;

            if (LogToFile_WriteMode == WriteMode.Stream)
            {
                if (LogToFile_OutputStream == null)
                {
                    LastException = new Exception("Output stream is not set !");
                    return;
                }
            }

            if (LogToFile_Mode != SaveFileMode.None)
            {
                var lines = data.Replace("\r", "").Split('\n');

                string output = string.Empty;
                string suffix = string.Empty;
                string level = log_level;

                if (level == "None" || level == string.Empty)
                {
                    level = string.Empty;
                }
                else
                {
                    level = $"[{level}] ".PadRight(8);
                }

                switch (LogToFile_SuffixMode)
                {
                    case Suffix_mode.None:
                        break;
                    case Suffix_mode.RunTime:
                        suffix = (DateTime.Now - creationTime).TotalSeconds.ToString("000000.000");
                        break;
                    case Suffix_mode.CurrentTime:
                        suffix = DateTime.Now.ToString("hh:mm:ss");
                        break;
                    case Suffix_mode.Custom:
                        suffix = LogToFile_CustomSuffix;
                        break;
                    default:
                        break;
                }

                if (suffix != string.Empty)
                {
                    suffix = $"[{suffix}] ";
                }

                foreach (var line in lines)
                {
                    output += $"{suffix}{level}{line}";
                }

                if (LogToFile_WriteMode == WriteMode.Stream)
                {
                    LogToFile_OutputStream.WriteData(output);
                }
                else
                {
                    File.AppendAllText(outputPath, output + Environment.NewLine);
                }
            }
        }
    }

    /// <summary>
    /// Manage logging
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// File path (default extension is .log if not set)
        /// </summary>
        public string LogToFile_FilePath { get; set; } = string.Empty;
        /// <summary>
        /// Log to file mode
        /// </summary>
        public SaveFileMode LogToFile_Mode { get; set; } = SaveFileMode.FileName_DatePrefix;
        /// <summary>
        /// Suffix of WriteLog text lines
        /// </summary>
        public Suffix_mode LogToFile_SuffixMode { get; set; } = Suffix_mode.RunTime;
        /// <summary>
        /// Custom suffix when LogToFile_SuffixMode is set to Suffix_mode.Custom
        /// </summary>
        public string LogToFile_CustomSuffix { get; set; } = "Custom text";
        /// <summary>
        /// Write mode
        /// </summary>
        public WriteMode LogToFile_WriteMode { get; set; } = WriteMode.Append;

        protected bool Enabled = false;

        public bool HasError { get; set; }

        public Exception LastException
        {
            get
            {
                HasError = false;
                return LastException;
            }
            protected set
            {
                HasError = true;
                LastException = value;
            }
        }

        /// <summary>
        /// Return %appdata%\Manufacturer\ProductName path
        /// </summary>
        public static string GetDefaultLogPath(string Manufacturer, string ProductName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"{Manufacturer}\{ProductName}\");
        }

        /// <summary>
        /// Output path generated by the Enable method and used by WriteLog
        /// </summary>
        protected string outputPath = string.Empty;

        // Get the creation time to then get the running time of the app
        protected DateTime creationTime;

        public enum SaveFileMode
        {
            /// <summary>
            /// Don't log to file
            /// </summary>
            None = 0,
            /// <summary>
            /// Log to specified FileName
            /// </summary>
            FileName = 1,
            /// <summary>
            /// Log to specified fileName with dateTime prefix
            /// </summary>
            FileName_DatePrefix = 2,
            /// <summary>
            /// Keep the 2 last logs name last and previous
            /// </summary>
            FileName_LastPrevious = 3
        }

        public enum Suffix_mode
        {
            /// <summary>
            /// No suffix
            /// </summary>
            None = 0,
            /// <summary>
            /// Time elapsed from the start
            /// </summary>
            RunTime = 1,
            /// <summary>
            /// Current time
            /// </summary>
            CurrentTime = 2,
            /// <summary>
            /// Custom text
            /// </summary>
            Custom = 3,
        }

        public enum Log_level
        {
            None = 0,
            Trace = 1,
            Debug = 2,
            Info = 3,
            Warn = 4,
            Error = 5,
            Fatal = 6,
        }

        public enum WriteMode
        {
            /// <summary>
            /// Write over existing file
            /// </summary>
            Write = 0,
            /// <summary>
            /// Append to exitsing file
            /// </summary>
            Append = 1,
            /// <summary>
            /// Output to a stream
            /// </summary>
            Stream = 2,
        }

        public Logger()
        {
            creationTime = DateTime.Now;
        }

        /// <summary>
        /// Check if writelog can be called safely AND generate final path. MUST be called BEFORE WriteLog
        /// </summary>
        public bool Enable()
        {
            switch (LogToFile_Mode)
            {
                case SaveFileMode.None:
                    break;
                case SaveFileMode.FileName:
                    // If string empty, invalid FilePath
                    if (LogToFile_FilePath == string.Empty)
                    {
                        return false;
                    }

                    // Set final output path
                    outputPath = LogToFile_FilePath;

                    break;
                case SaveFileMode.FileName_DatePrefix:
                    // If string empty, invalid FilePath
                    if (LogToFile_FilePath == string.Empty)
                    {
                        return false;
                    }

                    // Save extension
                    string extension = Path.GetExtension(LogToFile_FilePath);
                    // Remove extension
                    outputPath = Path.ChangeExtension(LogToFile_FilePath, null);
                    // Add dateTime
                    outputPath += "_" + DateTime.Now.ToString("yyyy_MM_dd__hh_mm_ss");
                    // Add extension
                    outputPath = Path.ChangeExtension(outputPath, extension);

                    break;
                case SaveFileMode.FileName_LastPrevious:
                    // If string empty, invalid FilePath
                    if (LogToFile_FilePath == string.Empty)
                    {
                        return false;
                    }

                    // Save extension
                    extension = Path.GetExtension(LogToFile_FilePath);
                    // Remove extension
                    outputPath = Path.ChangeExtension(LogToFile_FilePath, null);
                    // Add suffix
                    string previousPath = outputPath + "_previous";
                    outputPath += "_current";
                    // Add extension
                    outputPath = Path.ChangeExtension(outputPath, extension);
                    previousPath = Path.ChangeExtension(previousPath, extension);

                    // Rename last file
                    try
                    {
                        if (File.Exists(outputPath))
                        {
                            if (File.Exists(previousPath))
                            {
                                File.Delete(previousPath);
                            }
                            File.Move(outputPath, previousPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        LastException = ex;
                        return false;
                    }

                    break;
                default:
                    break;
            }

            outputPath = CheckFile(outputPath);

            creationTime = DateTime.Now;

            // If no invalid condition, return true
            Enabled = true;
            return true;
        }

        public void Disable()
        {
            Enabled = false;
        }

        protected virtual string CheckFile(string path)
        {
            if (LogToFile_WriteMode == WriteMode.Stream)
            {
                throw new ArgumentException("Invalid write mode. Use Logger<T> for WriteMode.Stream");
            }

            // Create directory if not existing
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            // If no extension, set .log
            if (!Path.HasExtension(path))
            {
                path = Path.ChangeExtension(path, "log");
            }

            // If file not existing, create
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            // Else if file existing and writemode set to write, clear file
            else
            {
                if (LogToFile_WriteMode == WriteMode.Write)
                {
                    File.WriteAllText(path, string.Empty);
                }
            }

            return path;
        }

        /// <summary>
        /// Write log with default level (debug)
        /// </summary>
        public void WriteLog(string data)
        {
            WriteLog(data, Log_level.Debug);
        }

        /// <summary>
        /// Write log with specified level
        /// </summary>
        public void WriteLog(string data, Log_level log_Level)
        {
            WriteLog(data, log_Level.ToString());
        }

        /// <summary>
        /// Write log with custom text
        /// </summary>
        public virtual void WriteLog(string data, string log_level)
        {
            if (LogToFile_WriteMode == WriteMode.Stream)
            {
                throw new ArgumentException("Invalid write mode. Use Logger<T> for WriteMode.Stream");
            }

            if (!Enabled)
                return;

            if (LogToFile_Mode != SaveFileMode.None)
            {
                var lines = data.Replace("\r", "").Split('\n');

                string output = string.Empty;
                string suffix = string.Empty;
                string level = log_level;

                if (level == "None" || level == string.Empty)
                {
                    level = string.Empty;
                }
                else
                {
                    level = $"[{level}] ".PadRight(8);
                }

                switch (LogToFile_SuffixMode)
                {
                    case Suffix_mode.None:
                        break;
                    case Suffix_mode.RunTime:
                        suffix = (DateTime.Now - creationTime).TotalSeconds.ToString("000000.000");
                        break;
                    case Suffix_mode.CurrentTime:
                        suffix = DateTime.Now.ToString("hh:mm:ss");
                        break;
                    case Suffix_mode.Custom:
                        suffix = LogToFile_CustomSuffix;
                        break;
                    default:
                        break;
                }

                if (suffix != string.Empty)
                {
                    suffix = $"[{suffix}] ";
                }

                foreach (var line in lines)
                {
                    output += $"{suffix}{level}{line}\n";
                }

                File.AppendAllText(outputPath, output);
            }
        }
    }

    public class StreamLogger<T>
    {
        private T stream;

        public StreamLogger(T stream)
        {
            this.stream = stream;
        }

        public void WriteData(string data)
        {
            Type streamType = stream.GetType();

            if (streamType.IsSubclassOf(typeof(Stream)))
            {
                byte[] bytes = Encoding.Default.GetBytes(data + Environment.NewLine);
                (stream as Stream).Write(bytes, 0, bytes.Length);
            }
            else if (streamType.IsSubclassOf(typeof(StreamWriter)))
            {
                (stream as StreamWriter).WriteLine(data);
            }
            else if (streamType.IsSubclassOf(typeof(TextWriter)))
            {
                (stream as TextWriter).WriteLine(data);
            }
            else
            {
                throw new ArgumentException("Unsupported stream type : " + streamType);
            }
        }


    }
}
