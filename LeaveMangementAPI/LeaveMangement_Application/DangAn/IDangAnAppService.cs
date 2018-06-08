using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.DangAn
{
    public interface IDangAnAppService
    {
        int GetUserCompId(string account);
        int GetUserDepId(string account);
        List<Company> GetCompanyList();
        Company GetCompanyById(int compId);
        object AddCompany(Company company);
        object EditCompany(Company company);
        object DeleteCompany(int compId);
        object SendMessage(string address);
        object GetDeparmentList(DepartmentDto query);
        object GetWorkList(WorkDto query);
        object AddSingleDpearment(AddSingleDeparmentDto dpearmentDto);
    }
}
