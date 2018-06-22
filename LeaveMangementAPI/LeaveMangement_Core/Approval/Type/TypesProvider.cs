using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Core.Approval.Type
{
    public class TypesProvider
    {
        public static readonly List<Types> _types = new List<Types>
        {
            new Types {Key="Type1",Id = 1,Name="事前" },
            new Types {Key="Type1",Id = 2,Name="事后" },
            new Types {Key="Type2",Id = 1,Name="病假"},
            new Types {Key="Type2",Id = 2,Name="事假" },
            new Types {Key="Type2",Id = 3,Name="年假" },
        };
    }
}
