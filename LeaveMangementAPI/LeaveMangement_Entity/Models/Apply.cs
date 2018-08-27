using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Models
{
    public partial class Apply
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int DeparmentId { get; set; }
        public int CompanyId { get; set; }
        public int Type1 { get; set; }
        public int Type2 { get; set; }
        public string Account { get; set; }
        public int? State { get; set; }
        public string Remark { get; set; }
        public int? LeaderId { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public long CreateTime { get; set; }
        public long? HandleTime { get; set; }
        public bool IsSubmit { get; set; }
        public bool IsRevoke { get; set; }
    }
}
