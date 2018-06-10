using LeaveMangement_Core.DangAn;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Models;
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
    }
}
