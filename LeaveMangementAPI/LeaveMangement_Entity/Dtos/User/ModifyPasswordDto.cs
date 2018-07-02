using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.User
{
    public class ModifyPasswordDto
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ReNewPassword { get; set; }
    }
}
