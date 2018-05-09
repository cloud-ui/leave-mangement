using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Core.User
{
    public class UserManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private UserService _userService = new UserService();
        public bool CreateCompanyAdmin(Company company)
        {
            string account = _userService.CreateAdmAccount();
            AdminUser user = new AdminUser()
            {
                Account = account,
                Name = company.Corporation,
                CompanyId = company.Id,
                Password = "123456",
                Status = "admin"
            };
            _ctx.AdminUser.Add(user);
            _ctx.SaveChanges();
            return _userService.SendEMail(company.Email,user);            
        }
    }
}
