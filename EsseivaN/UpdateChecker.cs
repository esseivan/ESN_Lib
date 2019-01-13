using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

/* Example :
 * 
 * private void CheckUpdates()
 *      {
 *          EsseivaN.Controls.UpdateChecker update = new EsseivaN.Controls.UpdateChecker("http://.../.../.../version.xml", this.ProductVersion);
 *          
 *          if (update.NeedUpdate())
 *          {   // Update available
 *              var result = update.Result;
 *              DialogResult dr = MessageBox.Show($"Update is available, do you want to visit the website ?\nCurrent : {result.CurrentVersion}\nLast : {result.LastVersion}", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
 *              if (dr == DialogResult.Yes)
 *                  result.OpenUpdateWebsite();
 *          }
 *          if (update.Result.ErrorOccured)
 *              throw update.Result.Error;
 *      }
 */

/* Version.xml file example
 * <?xml version="1.0" encoding="utf-8" ?>
 *	<Feed>
 *	<version>1.1.0</version>
 *	<url>http://www.website.com/softwares/publish.html</url>
 *	<silent>http://www.website.com/softwares/silentInstaller.msi</silent>
 *  <name>productName</name>
 *	</Feed>
 *	
 *	Silent installer is used to run a quick update
 */

namespace EsseivaN.Controls
{
    public class UpdateChecker : Component
    {
        /// <summary>
        /// Website where is located the file
        /// </summary>
        public string FileUrl { get; set; } = "";
        /// <summary>
        /// Result of the check
        /// </summary>
        public CheckUpdateResult Result { get; private set; } = new CheckUpdateResult();
        /// <summary>
        /// Actual version
        /// </summary>
        public string ProductVersion { get; set; }


        public UpdateChecker()
        {
        }

        public UpdateChecker(string FileUrl, string ProductVersion)
        {
            this.FileUrl = FileUrl;
            this.ProductVersion = ProductVersion;
        }

        /// <summary>
        /// Check for updates
        /// </summary>
        public bool CheckUpdates()
        {
            return UpdateCheck();
        }

        /// <summary>
        /// Check for updates
        /// </summary>
        public bool CheckUpdates(string FileUrl, string ProductVersion)
        {
            this.FileUrl = FileUrl;
            this.ProductVersion = ProductVersion;
            return UpdateCheck();
        }

        /// <summary>
        /// Indicate if an update is available
        /// </summary>
        public bool NeedUpdate()
        {
            UpdateCheck();
            return Result.NeedUpdate;
        }

        /// <summary>
        /// Indicate if an update is available
        /// </summary>
        public bool NeedUpdate(string FileUrl, string ProductVersion)
        {
            this.FileUrl = FileUrl;
            this.ProductVersion = ProductVersion;
            UpdateCheck();
            return Result.NeedUpdate;
        }

        /// <summary>
        /// Check if an update is available
        /// </summary>
        private bool UpdateCheck()
        {
            Result = new CheckUpdateResult();

            try
            {
                // Read file
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(FileUrl);
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();

                // Create xml
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                // Get tag version
                XmlNodeList versionNode = doc.GetElementsByTagName("version");
                if (versionNode.Count != 0)
                {
                    Result.lastVersion = new Version(versionNode[0].InnerText);
                    Result.currentVersion = new Version(ProductVersion);

                    // Check versions
                    if (Result.lastVersion > Result.currentVersion)
                    {   // Update available
                        Result.needUpdate = true;

                        // Get the download path
                        XmlNodeList urlNode = doc.GetElementsByTagName("url");
                        if (urlNode.Count != 0)
                        {
                            Result.updateURL = urlNode[0].InnerText;
                        }

                        // Get the silent update path
                        urlNode = doc.GetElementsByTagName("silent");
                        if (urlNode.Count != 0)
                        {
                            Result.silentUpdateURL = urlNode[0].InnerText;
                        }

                        // Get the filename
                        urlNode = doc.GetElementsByTagName("name");
                        if (urlNode.Count != 0)
                        {
                            Result.filename = urlNode[0].InnerText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result.errorOccured = true;
                Result.error = ex;
                return false;
            }
            return Result.NeedUpdate;
        }

        public class CheckUpdateResult
        {
            internal string silentUpdateURL;
            internal string updateURL;
            internal Version currentVersion;
            internal Version lastVersion;
            internal bool needUpdate = false;
            internal Exception error = null;
            internal bool errorOccured = false;
            internal string filename;

            public Version CurrentVersion { get => currentVersion; }
            public Version LastVersion { get => lastVersion; }
            public bool NeedUpdate { get => needUpdate; }
            public string UpdateURL { get => updateURL; }
            public Exception Error { get => error; }
            public bool ErrorOccured { get => errorOccured; }
            public string SilentUpdateURL { get => silentUpdateURL; }

            /// <summary>
            /// Open the website
            /// </summary>
            public void OpenUpdateWebsite()
            {
                Process.Start(updateURL);
            }

            /// <summary>
            /// Download and run the update
            /// </summary>
            public async Task<bool> DownloadUpdate()
            {
                return await DownloadUpdate(@"C:\temp\");
            }

            /// <summary>
            /// Download and run the update
            /// </summary>
            public async Task<bool> DownloadUpdate(string downloadPath)
            {
                WebClient webClient = new WebClient();
                await webClient.DownloadFileTaskAsync(new Uri(SilentUpdateURL), downloadPath + filename + ".msi");
                FileInfo info = new FileInfo(downloadPath + filename + ".msi");
                if (info.Length != 0)
                {
                    Process.Start(downloadPath + filename + ".msi");
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
