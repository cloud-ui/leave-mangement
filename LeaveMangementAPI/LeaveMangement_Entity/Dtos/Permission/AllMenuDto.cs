using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.Permission
{
    public class AllMenuDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<AllMenuDto> Children { get; set; }
        public string PositionId { get; set; }
    }
}
