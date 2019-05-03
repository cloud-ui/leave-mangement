using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.User
{
    public class EditUserMessageDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
