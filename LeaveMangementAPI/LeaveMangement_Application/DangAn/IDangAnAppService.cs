using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.DangAn
{
    public interface IDangAnAppService
    {
        List<Company> GetCompanyList();
        Company GetCompanyById(int compId);
        object AddCompany(Company company);
        object EditCompany(Company company);
        object DeleteCompany(int compId);
        object SendMessage(string phone);
        List<Deparment> GetDeparmentList(int compId);
    }
}
