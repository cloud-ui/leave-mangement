using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using System.Threading.Tasks;

namespace LeaveMangementAPI.Util
{
    public class FTPUtil
    {
        public FTPUtil() { }
        /// <summary>
        /// 连接FTP服务器函数
        /// </summary>
        /// <param name="strServer">服务器IP</param>
        /// <param name="strUser">用户名</param>
        /// <param name="strPassword">密码</param>
        public bool FTPIsConnected(string strServer, string strUser, string strPassword)
        {
            using (FtpClient ftp = new FtpClient())
            {
                ftp.Host = strServer;
                ftp.Credentials = new NetworkCredential(strUser, strPassword);
                ftp.Connect();
                return ftp.IsConnected;
            }
        }
        /// <summary>
        /// FTP下载文件
        /// </summary>
        /// <param name="strServer">服务器IP</param>
        /// <param name="strUser">用户名</param>
        /// <param name="strPassword">密码</param>
        /// <param name="Serverpath">服务器路径，例子："/Serverpath/"</param>
        /// <param name="fileName">所下载的文件名称</param>
        public FileStream FTPIsdownload(string strServer, string strUser, string strPassword, string Serverpath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(fileName, FileMode.Create);  //文件流，下载文件保存到客户端目录地址
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + strServer + Serverpath + fileName));  //创建FtpWebRequest对象，由Uri指定下载的文件
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(strUser, strPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];  //定义一个2Kb大小的数组
                int readCount = ftpStream.Read(buffer, 0, bufferSize);  //从当前流读取字节序列并存入buffer中，返回从流中读取的字节数
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);  //将buffer中的内容写到FileStream流中
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return outputStream;
            }
            catch (Exception ex)
            {
                return null;
            }
            //FtpClient ftp = new FtpClient();
            //ftp.Host = strServer;
            //ftp.Credentials = new NetworkCredential(strUser, strPassword);
            //ftp.Connect();
            //FileStream fileStream;
            //string path = Serverpath;
            //List<string> documentname = new List<string>();
            //#region  从FTP服务器下载文件
            //foreach (var ftpListItem in ftp.GetListing(path, FtpListOption.Modify | FtpListOption.Size)
            //  .Where(ftpListItem => string.Equals(ftpListItem.Name, fileName)))
            //{
            //    string destinationPath = string.Format(@"{0}", ftpListItem.Name);
            //    Stream ftpStream = ftp.OpenRead(ftpListItem.FullName);
            //    fileStream = new FileStream(destinationPath, FileMode.Create);
            //    //File.Create(destinationPath, (int)ftpStream.Length);
            //    var buffer = new byte[200 * 1024];
            //    int count;
            //    while ((count = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        fileStream.Write(buffer, 0, count);
            //    }
            //    documentname.Add(ftpListItem.Name);
            //    return fileStream;
            //}
            //#endregion
            //return null;
        }
    }
}
