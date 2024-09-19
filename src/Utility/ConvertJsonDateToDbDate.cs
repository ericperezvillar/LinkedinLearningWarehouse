using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.Utility
{
    public class ConvertJsonDateToDbDate
    {
        public static DateTime? UnixTimeStampToDateTime(long? unixTimeStamp)
        {
            if (!unixTimeStamp.HasValue) return null;

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp.Value);
            return dateTimeOffset.UtcDateTime;
        }
    }
}
