using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Model
{
    public partial class Deparment
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? ManagerId { get; set; }
        public string Name { get; set; }
        public int WorkerCount { get; set; }
        public string Code { get; set; }
    }
}
