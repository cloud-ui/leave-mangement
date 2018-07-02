using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.User
{
    public class Menus
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string PositionId { get; set; }
        public List<Menus> Children { get; set; }
        public string Icon { get; set; }
    }
}
