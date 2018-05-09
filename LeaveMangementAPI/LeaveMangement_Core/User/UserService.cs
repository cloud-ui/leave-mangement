using LeaveMangement_Entity.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

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
                for (int i = 0; i < 12; i++)
                {
                    account += random.Next(0, 12).ToString();
                }
            } while (_ctx.AdminUser.SingleOrDefault(u => u.Account.Equals(account)) != null);            
            return account;
        }

        public bool SendEMail(string mailAddress, AdminUser user)
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
