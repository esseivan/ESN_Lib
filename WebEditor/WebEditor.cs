using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WebsiteEditor;

namespace EsseivaN.Tools
{
    public class WebEditor
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var handle = GetConsoleWindow();

            frmMain frmMain = new frmMain();

            if (args.Length != 0)
            {
                Console.WriteLine("Importing and executing config files");
                foreach (string path in args)
                {
                    frmMain.ImportExecuteScript(path);
                }
                Console.WriteLine("Successfully executed scripts");
            }
            else
            {
                // Hide window
                ShowWindow(handle, SW_HIDE);
                Application.Run(new frmMain());
            }
        }
    }
}
