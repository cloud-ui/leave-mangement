using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos
{
    public class WorkDto
    {
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public string Query { get; set; }
        public int DepId { get; set; }
        public int CompId { get; set; }
    }
}
