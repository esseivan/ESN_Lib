using EsseivaN.Tools;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManualUpdateChecker
{
    static class ManualUpdateChecker
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string runPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string configPath = $"{runPath}\\config_version.txt";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if(!File.Exists(configPath))
            {
                MessageBox.Show("Config file not found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string data = File.ReadAllText(configPath).Replace("\r","");
            var datas = data.Split('\n');

            if(datas.Length < 2)
            {
                MessageBox.Show("Invalid config file", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string version = datas[0];
            string url = datas[1];
            bool silent = false;

            if(datas.Length >= 3)
            {
                if(datas[2] == "1")
                {
                    silent = true;
                }
            }

            Console.WriteLine("Silent mode : " + silent);
            Console.WriteLine("Current version : " + version);

            UpdateChecker updateChecker = new UpdateChecker(url, version);

            var task = CheckUpdate(updateChecker, silent);

            int state = 0;
            Console.WriteLine("Downloading   ");
            while(!(task.IsCompleted || task.IsCanceled))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("Downloading." + (state == 0 ? ("  "):(state == 1 ? ". " : "..")));
                Thread.Sleep(100);
                if (++state == 3)
                    state = 0;
            }
            Console.WriteLine("Complete !");
        }

        private static async Task CheckUpdate(UpdateChecker update, bool silent)
        {
            try
            {
                //MessageBox.Show(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
                update.CheckUpdates();
                if (update.Result.ErrorOccured)
                {
                    if (!silent)
                        MessageBox.Show(update.Result.Error.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        Console.Error.WriteLine($"ERROR : {update.Result.Error.ToString()}");
                    return;
                }

                if (update.NeedUpdate())
                {   // Update available
                    var result = update.Result;

                    Dialog.Dialog_SetButton(Dialog.Button.Button1, "Visit website");
                    Dialog.Dialog_SetButton(Dialog.Button.Button2, "Download and install");
                    Dialog.Dialog_SetButton(Dialog.Button.Button3, "Cancel");
                    Dialog.DialogResult dr = Dialog.ShowDialog($"Update is available, do you want to download ?\nCurrent : {result.CurrentVersion}\nLast : {result.LastVersion}",
                        "Update available",
                        Dialog.ButtonType.Custom1,
                        Dialog.ButtonType.Custom2,
                        Dialog.ButtonType.Custom3);

                    if (dr == Dialog.DialogResult.Custom1)
                    {
                        // Visit website
                        result.OpenUpdateWebsite();
                    }
                    else if (dr == Dialog.DialogResult.Custom2)
                    {
                        // Download and install
                        if (await result.DownloadUpdate())
                        {
                            return;
                        }
                        else
                        {
                            if (!silent)
                                MessageBox.Show("Unable to download update", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                Console.Error.WriteLine("ERROR : Unable to download update");
                        }
                    }
                }
                else
                {
                    if (!silent)
                        MessageBox.Show("No update avaiable", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        Console.WriteLine("Already up to date");
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show($"Unknown error :\n{ex}\n\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    Console.Error.WriteLine($"UNKNWON ERROR :\n{ex}\n\n{ex.StackTrace}");
            }
        }
    }
}
