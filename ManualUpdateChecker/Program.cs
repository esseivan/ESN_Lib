using EsseivaN.Tools;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManualUpdateChecker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string runPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string version = File.ReadAllText($"{runPath}\\version.txt");

            Console.WriteLine("Current version : " + version);

            UpdateChecker updateChecker = new UpdateChecker(@"http://www.esseivan.ch/files/esseivan/version.xml", version);

            var task = CheckUpdate(updateChecker);

            int state = 0;
            while(!(task.IsCompleted || task.IsCanceled))
            {
                Console.WriteLine("Downloading." + (state == 1 ? ("."):(state == 2 ? ".." : "")));
                Thread.Sleep(300);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }

        private static async Task CheckUpdate(UpdateChecker update)
        {
            try
            {
                //MessageBox.Show(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
                update.CheckUpdates();
                if (update.Result.ErrorOccured)
                {
                    MessageBox.Show(update.Result.Error.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Unable to download update", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No new release found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unknown error :\n{ex}\n\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
