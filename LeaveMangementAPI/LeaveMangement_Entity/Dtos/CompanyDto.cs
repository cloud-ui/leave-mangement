using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos
{
    public class CompanyDto
    {
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public string Query { get; set; }
    }
}
