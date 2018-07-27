using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Models
{
    public partial class Inform
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public long CreateTime { get; set; }
        public bool IsLook { get; set; }
        public int WorkId { get; set; }
        public int ApplicationId { get; set; }
    }
}
