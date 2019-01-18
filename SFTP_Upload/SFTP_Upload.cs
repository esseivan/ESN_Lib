using System;
using System.Collections.Generic;
using System.IO;
using WinSCP;

namespace EsseivaN.Tools
{
    public class SFTP_Upload
    {
        private static string username;
        private static bool HasUsername = false;
        private static string passphrase;
        private static bool HasPassphrase = false;
        private static string host;
        private static bool HasHost = false;
        private static string private_key;
        private static string host_key;
        private static bool HasKey = false;
        private static List<string[]> files = new List<string[]>();
        private static bool HasFolder = false;
        private static string helpText = @"upload folders's content using SFTP protocol
usage: <app_name> [-u username | -pk private_key_path | -hk host_key_path | -p passphrase | -h hostname |-f localFolder remoteFolder ]...
    options:
        -u username                     Set the username
        -pk private_key_path            Set the path to the private key file
        -hk host_key_path               Set the host key path
        -p passphrase                   Set the key passphrase
        -h hostname                     Set the host
        -f localFolder remoteFolder     Copy local folder to remote folder
";

        static void Main(string[] args)
        {
            // hijack to determine in WinScp is available
            try
            {
                Session temp = new Session();
                temp.Dispose();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("WinSCP not available ! Check if it is installer or install from the official website :\nwinscp.net");
                Console.ReadLine();
                Environment.Exit(-1);
                return;
            }

            // If no arguments, show help
            if (args.Length == 0)
            {
                Console.WriteLine(helpText);
            }
            else
            {
                ExecuteArgs(args);
                if (HasUsername && HasPassphrase && HasHost && HasFolder && HasKey)
                {
                    if(!RunUpload())
                    {
                        Console.WriteLine("Unable to upload");
                        Environment.Exit(2);
                    }
                }
                else
                {
                    Console.WriteLine("Arguments missing");
                    Environment.Exit(1);
                }
            }
        }

        /// <summary>
        /// Run the upload
        /// </summary>
        /// <returns>Wheter or not the upload is successfull</returns>
        public static bool RunUpload()
        {
            if (HasUsername && HasPassphrase && HasHost && HasFolder && HasKey)
            {
                try
                {
                    SessionOptions sessionOptions = new SessionOptions()
                    {
                        Protocol = Protocol.Sftp,
                        HostName = host,
                        UserName = username,
                        SshHostKeyFingerprint = host_key,
                        SshPrivateKeyPath = private_key,
                        PrivateKeyPassphrase = passphrase,
                        PortNumber = 22,
                        FtpMode = FtpMode.Passive,
                    };

                    using (Session session = new Session())
                    {
                        // Connect
                        session.Open(sessionOptions);

                        // Upload files
                        TransferOptions transferOptions = new TransferOptions();
                        transferOptions.TransferMode = TransferMode.Binary;

                        // Synchronize directories
                        foreach (string[] file in files)
                        {
                            Console.WriteLine($"Synchronize local {file[0]} to remote {file[1]}");
                            var result = session.SynchronizeDirectories(mode:SynchronizationMode.Remote, localPath:file[0], remotePath:file[1], removeFiles:false, mirror:false, criteria:SynchronizationCriteria.Either, options:transferOptions);
                            //result.Check();
                            Console.WriteLine("Failures: " + result.Failures.Count);
                            Console.WriteLine("Downloads: " + result.Downloads.Count);
                            Console.WriteLine("IsSuccess: " + result.IsSuccess);
                            Console.WriteLine("Uploads: " + result.Uploads.Count);
                            Console.WriteLine("Removals: " + result.Removals.Count);
                        }

                        // Session is automatically closed with the using keyword
                    }

                    Console.WriteLine("Upload success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Config not set !");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Give the arguments to fill the config
        /// </summary>
        public static void ExecuteArgs(string[] args)
        {
            for (short i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                if ((i + 1) < args.Length)
                {
                    switch (arg)
                    {
                        case "-u":  // Username
                            username = args[++i];
                            HasUsername = true;
                            break;
                        case "-p":  // Passphrase
                            passphrase = args[++i];
                            HasPassphrase = true;
                            break;
                        case "-h":  // Host name
                            host = args[++i];
                            HasHost = true;
                            break;
                        case "-pk":  // private key
                            private_key = args[++i];
                            HasKey = true;
                            break;
                        case "-hk":  // host key
                            host_key = File.ReadAllText(args[++i]);
                            HasKey = true;
                            break;
                        case "-f":  // Upload file (or folder)
                            if (i + 2 < args.Length)
                            {
                                string[] upload = new string[2];

                                upload[0] = System.IO.Path.GetDirectoryName(args[++i]);
                                upload[1] = args[++i];
                                files.Add(upload);
                                HasFolder = true;
                            }
                            break;
                        //case "-fa":  // Upload folder content
                        //    if (i + 2 < args.Length)
                        //    {
                        //        string[] upload = new string[2];

                        //        upload[0] = System.IO.Path.GetFullPath(args[++i]) + @"\*";
                        //        upload[1] = args[++i];
                        //        files.Add(upload);
                        //        HasFolder = true;
                        //    }
                        //    break;
                        default:    // Display help
                            Console.WriteLine("Unknown parameter : " + arg);
                            Console.WriteLine(helpText);
                            Environment.Exit(2);
                            break;
                    }
                }
                else // Command without parameter : none -> display help
                {
                    Console.WriteLine("Unknown parameter : " + arg);
                    Console.WriteLine(helpText);
                    Environment.Exit(2);
                }
            }
        }
    }
}
