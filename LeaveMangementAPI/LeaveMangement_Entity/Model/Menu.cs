using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Model
{
    public partial class Menu
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string PositionId { get; set; }
    }
}
