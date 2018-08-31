using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaveMangement_Core.Common
{
    public class CommonManager
    {
        private KaoQinContext _ctx = new KaoQinContext();

        public int GetUserCompId(string account)
        {
            return _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account)).CompanyId;
        }
        public int GetCompId(string componentName)
        {
            return _ctx.Company.SingleOrDefault(w => w.Name.Equals(componentName)).Id;
        }
        public int GetUserDepId(string account)
        {
            return _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account)).DepartmentId;
        }
        public int GetUserId(string userName)
        {
            return _ctx.Worker.SingleOrDefault(w => w.Name.Equals(userName)).Id;
        }

    }
}
