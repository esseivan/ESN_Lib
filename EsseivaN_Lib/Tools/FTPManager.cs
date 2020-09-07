using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsseivaN.Tools
{
    public class FTPManager
    {


        private void Upload()
        {
            FtpWebRequest request =
                (FtpWebRequest)WebRequest.Create("ftp://ftp.example.com/remote/path/file.zip");
            request.Credentials = new NetworkCredential("username", "password");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream fileStream = File.OpenRead(@"C:\local\path\file.zip"))
            using (Stream ftpStream = request.GetRequestStream())
            {
                progressBar1.Invoke(
                    (MethodInvoker)delegate { progressBar1.Maximum = (int)fileStream.Length; });

                byte[] buffer = new byte[10240];
                int read;
                while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ftpStream.Write(buffer, 0, read);
                    progressBar1.Invoke(
                        (MethodInvoker)delegate {
                            progressBar1.Value = (int)fileStream.Position;
                        });
                }
            }
        }
    }
}
