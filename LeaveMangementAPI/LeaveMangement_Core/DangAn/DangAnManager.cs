using LeaveMangement_Entity.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LeaveMangement_Core.User;

namespace LeaveMangement_Core.DangAn
{
    public class DangAnManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private UserManager _userManager = new UserManager();
        public List<Company> GetCompanyList()
        {
            return _ctx.Company.ToList();
        }
        public Company GetCompanyById(int compId)
        {
            return _ctx.Company.Find(compId);
        }
        public object AddCompany(Company company)
        {
            Company comp = _ctx.Company.SingleOrDefault(c => c.Name.Equals(company.Name));
            var result = new object();
            if (comp != null)
                result = new
                {
                    isSuccess = false,
                    message = "公司名字已经存在！"
                };
            else
            {
                //添加公司、自动创建一个管理员账号、发送邮件
                Company newComp = new Company()
                {
                    Name = company.Name,
                    CellphoneNumber = company.CellphoneNumber,
                    Corporation = company.Corporation,
                    Email = company.Email,
                    CreateTime = DateTime.Now,
                };
                _ctx.Company.Add(newComp);
                _ctx.SaveChanges();
                if (_userManager.CreateCompanyAdmin(newComp))
                    result = new
                    {
                        isSuccess = true,
                        message = "公司添加成功！"
                    };
                else
                    result = new
                    {
                        isSuccess = false,
                        message = "公司添加失败！"
                    };
            }
            return result;
        }
    }
}
