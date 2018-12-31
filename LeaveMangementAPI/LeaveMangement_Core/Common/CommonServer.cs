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
        public long MilliTimeStamp(long TheDatestr)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = DateTime.FromFileTime(TheDatestr).ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);

            return (long)ts.TotalMilliseconds;
        }
        public long ConvertTime(long milliTime)
        {
            long timeTricks = new DateTime(1970, 1, 1).Ticks + milliTime * 10000 + 
                TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
            return new DateTime(timeTricks).ToFileTime();
        }

    }
}
