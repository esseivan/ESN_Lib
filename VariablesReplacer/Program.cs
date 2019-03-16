using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsseivaN.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Missing arguments : Enter config path");
                return;
            }

            Console.WriteLine("Importing " + (args.Length) + " config file");
            foreach (string path in args)
            {
                Replacer.ImportConfig(path);
            }
            Console.WriteLine("Executing script !");
            Replacer.ExecuteScripts();
            Console.WriteLine("Complete !");
        }
    }
}
