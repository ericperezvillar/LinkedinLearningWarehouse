namespace LinkedinLearningWarehouse.Utility
{
    public class TransformJsonDataHelper
    {
        public static DateTime? UnixTimeStampToDateTime(long? unixTimeStamp)
        {
            if (!unixTimeStamp.HasValue) return null;

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp.Value);
            return dateTimeOffset.UtcDateTime;
        }

        public static long DateTimeToUnixTimeStamp(DateTime date)
        {            
            var unixTime = ((DateTimeOffset)date).ToUnixTimeMilliseconds();
            return unixTime;
        }
    }
}
