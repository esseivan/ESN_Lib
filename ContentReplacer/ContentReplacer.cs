using System;
using System.IO;

namespace EsseivaN.Tools
{
    public class ContentReplacer
    {
        static void Main(string[] args)
        {
            if(!RunEdit(args))
            {
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Edit the content of the specified file by the content of another specified file
        /// </summary>
        /// <param name="text">The text to be replaced</param>
        /// <param name="file_dest">The file to be modified</param>
        /// <param name="file_data">The file containing the data to replace the text</param>
        public static void EditByFile(string text, string file_dest, string file_data)
        {
            RunEdit(new string[] { "-f", text, file_dest, file_data });
        }

        /// <summary>
        /// Edit the content of the specified file by the specified data
        /// </summary>
        /// <param name="text">The text to be replaced</param>
        /// <param name="file_dest">The file to be modified</param>
        /// <param name="data">The data to replace the text</param>
        public static void EditByData(string text, string file_dest, string data)
        {
            RunEdit(new string[] { "-d", text, file_dest, data });
        }

        /// <summary>
        /// Run the edit from the arguments
        /// </summary>
        /// <returns>Wheter or not the arguments are valid</returns>
        public static bool RunEdit(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(@"Invalid use. Follow one of those examples :
    -f TextToReplace FileToReplace FileToPlace
    -d TextToReplace FileToReplace TextToPlace");
                return false;
            }

            string replace, replaceFile, contentFile, data, temp1, temp2;

            for (short i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-f":  // Replace by file content
                        replace = args[++i];
                        replaceFile = args[++i];
                        contentFile = args[++i];
                        if (!File.Exists(contentFile) || !File.Exists(replaceFile))
                        {
                            Console.WriteLine("Specified file doesn't exists !");
                            return false;
                        }
                        temp2 = File.ReadAllText(contentFile);
                        temp1 = File.ReadAllText(replaceFile).Replace(replace, temp2);
                        Console.WriteLine(replace);
                        Console.WriteLine(replaceFile);
                        Console.WriteLine(contentFile);
                        File.WriteAllText(replaceFile, temp1);
                        break;
                    case "-d":  // Replace by data
                        replace = args[++i];
                        replaceFile = args[++i];
                        data = args[++i];
                        if (!File.Exists(replaceFile))
                        {
                            Console.WriteLine("Specified file doesn't exists !");
                            return false;
                        }
                        Console.WriteLine(replace);
                        Console.WriteLine(replaceFile);
                        Console.WriteLine(data);
                        temp1 = File.ReadAllText(replaceFile).Replace(replace, data);
                        File.WriteAllText(replaceFile, temp1);
                        break;
                    default:
                        Console.WriteLine(@"Invalid use. Follow one of those examples :
    -f TextToReplace FileToReplace FileToPlace
    -d TextToReplace FileToReplace TextToPlace");
                        return false;
                }
            }

            return true;
        }
    }
}
