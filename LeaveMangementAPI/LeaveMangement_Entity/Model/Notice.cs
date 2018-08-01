using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Model
{
    public partial class Notice
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public long CreateTime { get; set; }
        public int CreateHuman { get; set; }
        public bool IsDelete { get; set; }
    }
}
