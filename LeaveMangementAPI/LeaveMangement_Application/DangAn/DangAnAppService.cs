using LeaveMangement_Core.DangAn;
using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.DangAn
{
    public class DangAnAppService : IDangAnAppService
    {
        private readonly DangAnManager _dangAnManager = new DangAnManager();
        public Company GetCompanyById(int compId)
        {
            return _dangAnManager.GetCompanyById(compId);
        }

        public List<Company> GetCompanyList()
        {
            return _dangAnManager.GetCompanyList();
        }
        public object AddCompany(Company company)
        {
            return _dangAnManager.AddCompany(company);
        }
        public object EditCompany(Company company)
        {
            return _dangAnManager.EditCompany(company);
        }
        public object DeleteCompany(int compId)
        {
            return _dangAnManager.DeleteCompany(compId);
        }
    }
}
