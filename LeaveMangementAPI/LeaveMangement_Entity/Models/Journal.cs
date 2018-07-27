using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Models
{
    public partial class Journal
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public string Content { get; set; }
        public long CreateTime { get; set; }
    }
}
