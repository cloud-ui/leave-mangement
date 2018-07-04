using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.DangAn
{
    public class EditCompanyDto
    {
        public int CompId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Corporation { get; set; }
        public string CellphoneNumber { get; set; }
    }
}
