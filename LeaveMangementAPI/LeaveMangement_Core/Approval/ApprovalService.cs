using LeaveMangement_Core.Approval.State;
using LeaveMangement_Core.Approval.Type;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace LeaveMangement_Core.Approval
{
    public class ApprovalService
    {
        //获取请假审批状态
        public string GetStateName(int? state)
        {
            List<States> allState = StateProvider._states;
            try
            {
                string stateName = allState.SingleOrDefault(s => s.Id == state).Name;
                return stateName;
            }
            catch
            {
                return "";
            }
            
        }
        //获取请假类别
        public string GetApplicationType(int type1, int type2)
        {
            List<Types> allTypes = TypesProvider._types;
            try
            {
                string type1Name = allTypes.SingleOrDefault(t => t.Id == type1 && t.Key.Equals("Type1")).Name;
                string type2Name = allTypes.SingleOrDefault(t => t.Id == type2 && t.Key.Equals("Type2")).Name;
                return type1Name + "-" + type2Name;
            }
            catch
            {
                return "";
            }
            
        }
        
    }
}
