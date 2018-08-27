using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Models
{
    public partial class ApplyFoJob
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int DeparmentId { get; set; }
        public int CompanyId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public long CreateTime { get; set; }
        public int? HandlerId { get; set; }
        public long? HandlTime { get; set; }
        public bool? Result { get; set; }
    }
}
