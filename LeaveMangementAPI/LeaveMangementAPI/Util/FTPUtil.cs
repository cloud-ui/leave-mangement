using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LeaveMangementAPI.Util
{
    public class FTPUtil
    {
        public FTPUtil() { }
        //public void DownFile(string filePath, string fileName, HttpContext httpContext)
        //{
        //    // filePath  文件路径 例如：/File/记录.xlsx 

        //    // fileName  文件名称 例如：记录.xlsx （要后缀哦）
        //    Encoding encoding; // 申明编码
        //    string outputFileName; // 输出名字
        //    Debug.Assert(httpContext.Request.UserAgent != null, "HttpContext.ApplicationInstance.Request.UserAgent != null");
        //    string browser = httpContext.Request.UserAgent.ToUpper();
        //    // 微软的浏览器和ie过滤
        //    if (browser.Contains("MS") && browser.Contains("IE"))
        //    {
        //        outputFileName = HttpUtility.UrlEncode(filePath);
        //        encoding = Encoding.Default;
        //    }
        //    //火狐
        //    else if (browser.Contains("FIREFOX"))
        //    {
        //        outputFileName = fileName;
        //        encoding = Encoding.GetEncoding("GB2312");
        //    }
        //    else
        //    {
        //        outputFileName = HttpUtility.UrlEncode(fileName);
        //        encoding = Encoding.Default;
        //    }

        //    //string absoluFilePath = Server.MapPath(filePath); //获取上传文件路径
        //    //FileStream fs = new FileStream(absoluFilePath, FileMode.Open);
        //    FileStream fs = new FileStream("D:/Code/JoyCode/leave-mangement/LeaveMangementAPI/LeaveMangementAPI/files/test.txt", FileMode.Open);
        //    byte[] bytes = new byte[(int)fs.Length];
        //    fs.Read(bytes, 0, bytes.Length);
        //    fs.Close(); //关闭流，释放资源
        //    httpContext.Response.Clear();
        //    httpContext.Response.Buffer = true;
        //    httpContext.Response.ContentEncoding = encoding;
        //    httpContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", string.IsNullOrEmpty(outputFileName) ? DateTime.Now.ToString("yyyyMMddHHmmssfff") : outputFileName));
        //    httpContext.Response.BinaryWrite(bytes);
        //    Response.Flush();
        //    httpContext.ApplicationInstance.Response.End();
        //}
    }
}
