using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Upload
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        public void RunUpload(BaseConfig config)
        {
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential(config.Username, config.Password);
                client.UploadFile("ftp://host/path.zip", WebRequestMethods.Ftp.UploadFile, localFile);
            }

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.example.com/remote/path/file.zip");
            request.Credentials = new NetworkCredential("username", "password");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream fileStream = File.OpenRead(@"C:\local\path\file.zip"))
            using (Stream ftpStream = request.GetRequestStream())
            {
                byte[] buffer = new byte[10240];
                int read;
                while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ftpStream.Write(buffer, 0, read);
                    Console.WriteLine("Uploaded {0} bytes", fileStream.Position);
                }
            }
        }

        public class BaseConfig
        {
            /// <summary>
            /// Hostname
            /// </summary>
            public string Hostname { get; set; }

            /// <summary>
            /// Username
            /// </summary>
            public string Username { get; set; }

            /// <summary>
            /// Passphrase for private key
            /// </summary>
            public string Password { get; set; }

            /// <summary>
            /// Port to be used
            /// </summary>
            public int Port { get; set; } = 22;

            /// <summary>
            /// List of configs
            /// </summary>
            public Config[] Configs { get; set; }

            /// <summary>
            /// Check wheter all parameters are set
            /// </summary>
            /// <returns></returns>
            public bool IsValid()
            {
                if (string.IsNullOrEmpty(Hostname) ||
                    string.IsNullOrEmpty(Username) ||
                    string.IsNullOrEmpty(Password) ||
                    string.IsNullOrEmpty(PrivateKeyPath) ||
                    string.IsNullOrEmpty(HostKeyPath))
                    return false;
                return true;
            }

            public class Config : IComparable<Config>
            {
                /// <summary>
                /// Is this config enabled
                /// </summary>
                public bool Enabled { get; set; } = true;
                /// <summary>
                /// Priority to run this config, the higher this value, the higher the priority
                /// </summary>
                public int RunPriority { get; set; }
                /// <summary>
                /// Determine the synchronization mode. Default : Remote. Local = 1, Remote = 2, Both = 3
                /// </summary>
                public SynchronizationMode SyncMode { get; set; } = SynchronizationMode.Remote;
                /// <summary>
                /// Determine the synchronization criteria. Default : Time
                /// </summary>
                public SynchronizationCriteria SyncCriteria { get; set; } = SynchronizationCriteria.Time;
                /// <summary>
                /// Determine the transfer mode. Default : Binary
                /// </summary>
                public TransferMode TransferMode { get; set; } = TransferMode.Binary;
                /// <summary>
                /// Local path
                /// </summary>
                public string LocalPath { get; set; }
                /// <summary>
                /// FileMask for the transfer
                /// </summary>
                public string FileMask { get; set; }
                /// <summary>
                /// Remote path
                /// </summary>
                public string RemotePath { get; set; }
                /// <summary>
                /// Remove files. True to remove, False to keep
                /// </summary>
                public bool RemoveFiles { get; set; } = false;
                /// <summary>
                /// Mirror mode
                /// </summary>
                public bool Mirror { get; set; } = false;
                /// <summary>
                /// Is sync mode enabled or are we just uploading files wihout checking. True = sync mode, False = upload mode
                /// </summary>
                public bool SyncModeEnabled { get; set; } = true;

                /// <summary>
                /// Compare according to run priority (0 is lowest priority, negatives excluded)
                /// </summary>
                public int CompareTo(Config other)
                {
                    // Negatives numbers are last priority
                    if (this.RunPriority < 0)
                    {
                        // If also last priority, set equal
                        if (other.RunPriority < 0)
                            return 0;
                        return -1;
                    }
                    // Priority close to 0 comes first
                    if (this.RunPriority < other.RunPriority)
                        return 1;
                    if (this.RunPriority > other.RunPriority)
                        return -1;
                    return 0;
                }

                /// <summary>
                /// Indicate if LocalPath and RemotePath are set (don't check if they exists)
                /// </summary>
                /// <returns></returns>
                public bool IsValid()
                {
                    if (LocalPath == null || RemotePath == null)
                        return false;
                    if (LocalPath == string.Empty || RemotePath == string.Empty)
                        return false;
                    return true;
                }

                public override string ToString()
                {
                    return $"Config@{GetHashCode()} []";
                }
            }
        }
    }
}
