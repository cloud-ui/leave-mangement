using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Models;
using System.Collections.Generic;

namespace LeaveMangement_Application.DangAn
{
    public interface IDangAnAppService
    {
        
        List<Company> GetCompanyList();
        Company GetCompanyById(int compId);
        object AddCompany(Company company);
        object EditCompany(EditCompanyDto editCompanyDto);
        object DeleteCompany(int compId);
        object SendMessage(string address);
        object GetDeparmentList(DepartmentDto query);
        object GetDeparments(int compId);
        object GetWorkList(WorkDto query);
        object GetWorkerList(int compId);
        object AddSingleDpearment(AddSingleDeparmentDto dpearmentDto);
        object EditDeparment(AddSingleDeparmentDto addSingleDeparmentDto);
        object DeleteDeparment(int id);
        object GetDeparmentById(int id);
        object GetPositionListByCompId(int compId);
        object DeletePosition(int id);
        object AddPosition(AddStateDto addStateDto);
        object EditPosition(AddStateDto addStateDto);
        object GetStateListByCompId(int compId);
        object DeleteState(int id);
        object AddState(AddStateDto addStateDto);
        object EditState(AddStateDto addStateDto);
        Result TransferWorker(TransferWorkerDto transferWorkerDto);
    }
}
