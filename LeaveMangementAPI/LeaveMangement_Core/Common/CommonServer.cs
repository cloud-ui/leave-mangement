using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Core.Common
{
    public class CommonServer
    {
        public string ChangeTime(long time)
        {
            DateTime dt = DateTime.FromFileTime(time);
            return dt.ToString("yyyy-MM-dd");
        }
    }
}
