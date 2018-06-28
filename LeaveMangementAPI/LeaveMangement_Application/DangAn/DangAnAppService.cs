using LeaveMangement_Core.DangAn;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Model;
using System.Collections.Generic;

namespace LeaveMangement_Application.DangAn
{
    public class DangAnAppService : IDangAnAppService
    {
        private readonly DangAnManager _dangAnManager = new DangAnManager();
        private readonly DangAnService _dangAnService = new DangAnService();
       
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
        public object SendMessage(string address)
        {
            return _dangAnService.SendAuthCodeEMail(address);
        }
        public object GetDeparmentList(DepartmentDto query)
        {
            return _dangAnManager.GetDeparmentList(query);
        }
        public object AddSingleDpearment(AddSingleDeparmentDto deparmentDto)
        {
            return _dangAnManager.AddSingleDpearment(deparmentDto);
        }
        public object GetWorkList(WorkDto query)
        {
            return _dangAnManager.GetWorkerList(query);
        }
        public object GetWorkerList(int compId)
        {
            return _dangAnManager.GetWorkerList(compId);
        }
        public object GetPositionListByCompId(int compId)
        {
            return _dangAnManager.GetPositionListByCompId(compId);
        }
        public object DeletePosition(int id)
        {
            return _dangAnManager.DeletePosition(id);
        }
        public object AddPosition(AddStateDto addStateDto)
        {
            return _dangAnManager.AddPosition(addStateDto);
        }
        public object EditPosition(AddStateDto addStateDto)
        {
            return _dangAnManager.EditPosition(addStateDto);
        }
        public object GetStateListByCompId(int compId)
        {
            return _dangAnManager.GetStateListByCompId(compId);
        }
        public object DeleteState(int id)
        {
            return _dangAnManager.DeleteState(id);
        }
        public object AddState(AddStateDto addStateDto)
        {
            return _dangAnManager.AddState(addStateDto);
        }
        public object EditState(AddStateDto addStateDto)
        {
            return _dangAnManager.EditState(addStateDto);
        }
    }
}
