using LeaveMangement_Entity.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LeaveMangementAPI.Util
{
    public class ImportExcelUtil
    {
        public ImportExcelUtil() { }
        public object SaveExcel(IFormFile file)
        {
            string completePath = "";
            String Tpath = DateTime.Now.ToString("yyyy-MM-dd") + @"/";
            string filename = DateTime.Now.ToFileTime() + file.FileName;
            //string FilePath = "D:\\" + Tpath + filename;              //
            string FilePath = (@"./files/upload/") + Tpath + filename;
            //这里应该获取当前项目路径地址，再在后面创建文件，如果按上面的注释掉的写法，在服务器上没有找到d盘，则会报错。
            string diPath = Path.GetDirectoryName(FilePath);    //获取到当前目录的文件夹，没有就创建

            if (!Directory.Exists(diPath)) { Directory.CreateDirectory(diPath); };
            try
            {
                completePath = FilePath;
                using (Stream stream = file.OpenReadStream())
                using (FileStream fileStream = System.IO.File.Create(completePath, (int)stream.Length))
                {
                    var buffer = new byte[200 * 1024];
                    int count;
                    while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, count);
                    }
                }
                return completePath;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public bool JudgeCol(ExcelWorksheet worksheet,string[] colName)
        {
            int ColCount = worksheet.Dimension.Columns;
            bool bHeaderRow = true;
            //excel下标从1开始
            for (int col = 1,index=0; col <= ColCount&&index<colName.Length; col++,index++)
            {
                string c = worksheet.Cells[1, col].Value.ToString();
                if (!c.Equals(colName[index]))
                {
                    bHeaderRow = false;
                    throw new Exception("格式错误，导入文件的第"+index+"列应为"+colName[index]+"！");
                }
            }
            return bHeaderRow;
        }
        ///<summary>
        /// 写入数据到数据库
        /// </summary>
        /// <param name="completePath">文件在项目的存放地址</param>
        /// <returns></returns>
        //public Object ImportPrize(string completePath)
        //{
        //    var ret = new object();
        //    List<string> errors = new List<string>();
        //    List<string> fails = new List<string>();
        //    int succescount = 0;
        //    int failcount = 0;

        //    try
        //    {
        //        var a = ReadExcelToTable(completePath).Rows;
        //        List<Deparment> list = new List<Deparment>();
        //        if (a.Count > 1)
        //        {
        //            for (var i = 0; i < a.Count; i++)
        //            {
        //                if (i == 0)
        //                {
        //                    #region 判断列名
        //                    var col_one = a[i][0].ToString();
        //                    if (col_one != "版本")
        //                    {
        //                        throw new Exception("格式错误，导入文件的第一列应为【版本】！");
        //                    }
        //                    //var col_two = a[i][1].ToString();
        //                    //if (col_two != "版本状态")
        //                    //{
        //                    //    throw new Exception("格式错误，导入文件的第一列应为【版本状态】！");
        //                    //}
        //                    var col_three = a[i][1].ToString();
        //                    if (col_three != "检测机构检测项")
        //                    {
        //                        throw new Exception("格式错误，导入文件的第一列应为【检测机构检测项】！");
        //                    }
        //                    var col_five = a[i][2].ToString();
        //                    if (col_five != "区域市场")
        //                    {
        //                        throw new Exception("格式错误，导入文件的第一列应为【区域市场】！");
        //                    }
        //                    var col_six = a[i][3].ToString();
        //                    if (col_six != "区域市场价格")
        //                    {
        //                        throw new Exception("格式错误，导入文件的第一列应为【区域市场价格】！");
        //                    }
        //                    var col_even = a[i][4].ToString();
        //                    if (col_even != "VIP零售价格")
        //                    {
        //                        throw new Exception("格式错误，导入文件的第一列应为【VIP零售价格】！");
        //                    }

        //                    #endregion
        //                }
        //                else
        //                {
        //                    //int intType2; var intTypeStr2 = a[i][1].ToString();
        //                    //if (!int.TryParse(intTypeStr2, out intType2))
        //                    //{
        //                    //    throw new Exception("格式错误，【版本状态】【第" + (i + 1) + "行】应为整数类型数据！");
        //                    //}
        //                    int intType1; var intStr1 = a[i][1].ToString();
        //                    if (!int.TryParse(intStr1, out intType1))
        //                    {
        //                        throw new Exception("格式错误，【检测机构检测项】【第" + (i + 1) + "行】应为整数类型数据！");
        //                    }
        //                    int intType; var intStr = a[i][2].ToString();
        //                    if (!int.TryParse(intStr, out intType))
        //                    {
        //                        throw new Exception("格式错误，【区域市场】【第" + (i + 1) + "行】应为整数类型数据！");
        //                    }
        //                    decimal docmoney_int; var docmoney_str = a[i][3].ToString();
        //                    if (!decimal.TryParse(docmoney_str, out docmoney_int))
        //                    {
        //                        throw new Exception("格式错误，【区域市场价格】【第" + (i + 1) + "行】应为数字类型数据！");
        //                    }

        //                    decimal doczmoney_int; var doczmoney_str = a[i][4].ToString();
        //                    if (!decimal.TryParse(doczmoney_str, out doczmoney_int))
        //                    {
        //                        throw new Exception("格式错误，【VIP零售价格】【第" + (i + 1) + "行】应为数字类型数据！");
        //                    }
        //                    //list.Add(new Deparment()
        //                    //{
        //                    //    Version = a[i][0].ToString(),
        //                    //    //VersionState = false,
        //                    //    DetectionOrgDetectionItemID = intType1,
        //                    //    AreaMarketID = intType,
        //                    //    Price = docmoney_int,
        //                    //    vipPrice = doczmoney_int
        //                    //});
        //                }
        //            }

        //            #region ListForEach

        //            //using (YZS_BUSEntities context = new YZS_BUSEntities())
        //            //{
        //            //    foreach (var item in list)
        //            //    {
        //            //        var entities = context.Set<区域产品信息>().Where(n => n.检测机构检测项.Value == item.DetectionOrgDetectionItemID && n.区域市场.Value == item.AreaMarketID && n.版本状态.Value == false).ToList();
        //            //        if (entities.Count() > 0)
        //            //        {
        //            //            fails.Add("检测项:" + item.DetectionOrgDetectionItemID + "区域市场:" + item.AreaMarketID);
        //            //            failcount++;
        //            //        }
        //            //        else
        //            //        {
        //            //            context.区域产品信息.Add(new 区域产品信息()
        //            //            {
        //            //                版本 = item.Version,
        //            //                版本状态 = false,
        //            //                检测机构检测项 = item.DetectionOrgDetectionItemID,
        //            //                区域市场 = item.AreaMarketID,
        //            //                区域市场价格 = item.Price,
        //            //                VIP零售价格 = item.vipPrice
        //            //            });
        //            //            succescount++;
        //            //        }
        //            //    }
        //            //    context.SaveChanges();
        //            //    string failRemark = "";
        //            //    if (failcount > 0)
        //            //    {
        //            //        failRemark = ",失败的数据：" + string.Join(",", fails);
        //            //    }
        //            //    errors.Add($"成功导入{succescount}条样本信息！失败{failcount}条{failRemark}");
        //            //}

        //            //ret.isOK = true;
        //            //ret.errorCode = 0;
        //            //ret.msg = "";
        //            //ret.count = succescount;
        //            //ret.datas = errors;

        //            #endregion
        //        }
        //        else
        //        {
        //            throw new Exception("所选文件格式错误，或者未匹配到有效数据！");
        //        }
        //    }
        //    catch (Exception error)
        //    {
        //        //ret.isOK = false;
        //        //ret.errorCode = 200;
        //        //ret.msg = error.Message;
        //        //ret.count = 0;
        //        //ret.datas = errors;
        //    }
        //    return ret;
        //}

        //private void ReadExcelToTable(string path)
        //{
        //    string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + path + "';" +
        //    "Extended Properties='Excel 8.0;IMEX=1'";
        //    DataSet dsMin = new DataSet();
        //    OleDbDataAdapter oada = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
        //    //oada.Fill(dsMin);
        //}

    }
}
