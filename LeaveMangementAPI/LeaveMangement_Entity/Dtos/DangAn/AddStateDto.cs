using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.DangAn
{
    public class AddStateDto
    {
        public string Name { get; set; }
        public int CompId { get; set; }
        public int Pay { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
    }
}
