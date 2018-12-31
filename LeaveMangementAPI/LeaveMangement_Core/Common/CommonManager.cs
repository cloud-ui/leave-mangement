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
            Company company = _ctx.Company.SingleOrDefault(w => w.Name.Equals(componentName));
            return company != null ? company.Id : 0;
        }
        public int GetDepId(string depName)
        {
            Deparment deparment = _ctx.Deparment.SingleOrDefault(w => w.Name.Equals(depName));
            return deparment != null ? deparment.Id : 0;
        }
        public int GetUserDepId(string account)
        {
            return _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account)).DepartmentId;
        }
        public int GetUserId(string userName)
        {
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.Name.Equals(userName));
            return worker != null ? worker.Id : 0;
        }
        public int GetPaperType(string paperType)
        {
            return _ctx.PaperType.SingleOrDefault(p => p.Name.Equals(paperType)).Id;
        }
        public int GetState(string name,int compId)
        {
            State state = _ctx.State.SingleOrDefault(s => s.Name.Equals(name) && s.CompanyId == compId);
            return state != null ? state.Id : 0;
        }
        public int GetPosition(string positionName,int compId)
        {
            Position position = _ctx.Position.SingleOrDefault(p => p.CompanyId == compId && p.Name.Equals(positionName));
            return position != null ? position.Id : 0;
        }
        /// <summary>
        /// 批量添加部门时，修改部门人数
        /// 1:根据部门编号分组
        /// 2：对分组进行遍历
        /// 3：对每个分组的员工的职位进行判断，如果是经理，就讲部门经理进行修改
        /// </summary>
        /// <param name="workers"></param>
        public void ChangeDepWorkerCount(List<Worker> workers)
        {
            var result = (from worker in workers
                          group worker by worker.DepartmentId).ToList();  
            Deparment deparment;
            foreach(var item in result)
            {
                deparment = _ctx.Deparment.Find(item.Key);
                int i = item.Count();
                deparment.WorkerCount = deparment.WorkerCount + item.Count();
                foreach(Worker worker in item)
                {
                    if (_ctx.Position.Find(worker.PositionId).Name.Contains("部门经理"))
                        deparment.ManagerId = worker.Id;
                }
                _ctx.SaveChanges();
            }
        }
        public bool IsExitDep(string depName,int companyId)
        {
            Deparment deparment = _ctx.Deparment.SingleOrDefault(d => d.Name.Equals(depName) && d.CompanyId == companyId);
            return deparment != null ? true : false;
        }
        public bool IsExitWorker(int? paperType,string paperNumber,int compId)
        {
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.CompanyId == compId && w.PaperType == paperType && w.PaperNumber.Equals(paperNumber));
            return worker != null ? true : false;
        }

    }
}
