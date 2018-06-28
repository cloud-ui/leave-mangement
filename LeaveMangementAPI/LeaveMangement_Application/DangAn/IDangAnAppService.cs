using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Model;
using System.Collections.Generic;

namespace LeaveMangement_Application.DangAn
{
    public interface IDangAnAppService
    {
        
        List<Company> GetCompanyList();
        Company GetCompanyById(int compId);
        object AddCompany(Company company);
        object EditCompany(Company company);
        object DeleteCompany(int compId);
        object SendMessage(string address);
        object GetDeparmentList(DepartmentDto query);
        object GetWorkList(WorkDto query);
        object GetWorkerList(int compId);
        object AddSingleDpearment(AddSingleDeparmentDto dpearmentDto);
        object GetPositionListByCompId(int compId);
        object DeletePosition(int id);
        object AddPosition(AddStateDto addStateDto);
        object EditPosition(AddStateDto addStateDto);
        object GetStateListByCompId(int compId);
        object DeleteState(int id);
        object AddState(AddStateDto addStateDto);
        object EditState(AddStateDto addStateDto);
    }
}
