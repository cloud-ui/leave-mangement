using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.DangAn
{
    public class ExcelDep
    {
        public string Company { get; set; }
        public string Manager { get; set; }
        public string Name { get; set; }
        public int WorkerCount { get; set; }
        public string Code { get; set; }
        public bool IsSuccess { get; set; }
    }
}
