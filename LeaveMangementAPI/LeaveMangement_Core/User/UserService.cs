
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using LeaveMangement_Entity.Models;

namespace LeaveMangement_Core.User
{
    public class UserService
    {
        private KaoQinContext _ctx = new KaoQinContext();
        public string CreateAdmAccount()
        {
            Random random = new Random(GetRandomSeedbyGuid());
            string account;
            do
            {
                account = "";
                for (int i = 0; i < 10; i++)
                {
                    account += random.Next(0, 10).ToString();
                }
            } while (_ctx.Worker.SingleOrDefault(u => u.Account.Equals(account)) != null);            
            return account;
        }
        /// <summary>
        /// 根据身份证获取年龄
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public int GetAgeFromIdCard(string idCard)
        {
            int age = 0;
            if (!string.IsNullOrWhiteSpace(idCard))
            {
                var subStr = string.Empty;
                if (idCard.Length == 18)
                {
                    subStr = idCard.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                }
                else if (idCard.Length == 15)
                {
                    subStr = ("19" + idCard.Substring(6, 6)).Insert(4, "-").Insert(7, "-");
                }
                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(subStr));
                age = ts.Days / 365;
            }
            return age;
        }
        public long GetBirthdayFromIdCard(string idCard)
        {
            string birthday = "";
            if (idCard.Length == 18)
            {
                birthday = idCard.Substring(6, 4) + "-" + idCard.Substring(10, 2) + "-" + idCard.Substring(12, 2);

            }

    //处理15位的身份证号码从号码中得到生日
            if (idCard.Length == 15)
            {
                birthday = "19" + idCard.Substring(6, 2) + "-" + idCard.Substring(8, 2) + "-" + idCard.Substring(10, 2);
            }
            return DateTime.Parse(birthday).ToFileTime();
        }

        public bool SendEMail(string mailAddress, Worker user)
        {
            try
            {
                MailMessage eMail = new MailMessage();
                eMail.To.Add(mailAddress);
                eMail.From = new MailAddress("13628471426@163.com", "人事考勤系统", System.Text.Encoding.UTF8);
                eMail.Subject = "公司添加成功邮件";
                eMail.SubjectEncoding = System.Text.Encoding.UTF8;
                eMail.Body = "尊敬的"+user.Name+"您好！您的公司已在XX人事系统注册成功!账号："+user.Account+"默认密码："+user.Password+"</br><a href='http://192.168.199.233:52675/api/User/CheckRegister?account=" + mailAddress + "'>点击这里登录</a></br>";
                eMail.BodyEncoding = System.Text.Encoding.UTF8;
                eMail.IsBodyHtml = true;
                eMail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential("13628471426@163.com", "jxzxc1230");
                client.Host = "smtp.163.com";
                client.Port = 25;
                client.EnableSsl = true;
                client.Send(eMail);
                return true;
            }
            catch
            {
                return false;
            }
        }
        int GetRandomSeedbyGuid()
        {
            return new Guid().GetHashCode();
        }
    }
}
