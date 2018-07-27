using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.Notices
{
    public class AddNotice
    {
        public string Title { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
    }
}
