using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
 *	</Feed>
 */

namespace EsseivaN.Controls
{
    public class UpdateChecker : Component
    {
        private string fileUrl = "";
        private CheckUpdateResult result = new CheckUpdateResult();
        private string productVersion;

        /// <summary>
        /// Website where is located the file
        /// </summary>
        public string FileUrl { get => fileUrl; set => fileUrl = value; }
        /// <summary>
        /// Result of the check
        /// </summary>
        public CheckUpdateResult Result { get => result; }
        /// <summary>
        /// Actual version
        /// </summary>
        public string ProductVersion { get => productVersion; set => productVersion = value; }


        public UpdateChecker()
        {
        }

        public UpdateChecker(string FileUrl, string ProductVersion)
        {
            fileUrl = FileUrl;
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
            fileUrl = FileUrl;
            this.ProductVersion = ProductVersion;
            return UpdateCheck();
        }

        /// <summary>
        /// Indicate if an update is available
        /// </summary>
        public bool NeedUpdate()
        {
            UpdateCheck();
            return result.NeedUpdate;
        }

        /// <summary>
        /// Indicate if an update is available
        /// </summary>
        public bool NeedUpdate(string FileUrl, string ProductVersion)
        {
            fileUrl = FileUrl;
            this.ProductVersion = ProductVersion;
            UpdateCheck();
            return result.NeedUpdate;
        }

        /// <summary>
        /// Check if an update is available
        /// </summary>
        private bool UpdateCheck()
        {
            result = new CheckUpdateResult();

            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(FileUrl);
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                XmlNodeList versionNode = doc.GetElementsByTagName("version");
                if (versionNode.Count != 0)
                {
                    result.lastVersion = new Version(versionNode[0].InnerText);
                    result.currentVersion = new Version(ProductVersion);

                    if (result.lastVersion > result.currentVersion)
                    {   // Update available
                        result.needUpdate = true;

                        XmlNodeList urlNode = doc.GetElementsByTagName("url");
                        if (urlNode.Count != 0)
                        {
                            result.updateURL = urlNode[0].InnerText;
                        }

                        urlNode = doc.GetElementsByTagName("silent");
                        if (urlNode.Count != 0)
                        {
                            result.silentUpdateURL = urlNode[0].InnerText;
                        }

                        urlNode = doc.GetElementsByTagName("name");
                        if (urlNode.Count != 0)
                        {
                            result.filename = urlNode[0].InnerText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorOccured = true;
                result.error = ex;
                return false;
            }
            return result.NeedUpdate;
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

            public string GetPath() => silentUpdateURL;

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
                WebClient webClient = new WebClient();
                await webClient.DownloadFileTaskAsync(new Uri(SilentUpdateURL), @"C:\temp\" + filename + ".msi");
                FileInfo info = new FileInfo(@"C:\temp\" + filename + ".msi");
                if (info.Length != 0)
                {
                    Process.Start(@"C:\temp\" + filename + ".msi");
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
