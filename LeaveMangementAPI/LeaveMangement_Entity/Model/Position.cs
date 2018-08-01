using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Model
{
    public partial class Position
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int Pay { get; set; }
        public int ParentId { get; set; }
    }
}
