using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Core.Approval.approvalState
{
    public class StateProvider
    {
        public static readonly List<States> _states = new List<States>
        {
            new States {Id = 1,Name="未审批" },
            new States {Id = 2,Name="已批准" },
            new States {Id = 3,Name="未批准"},
        };
    }
}
