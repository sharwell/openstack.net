namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;

    internal static class DateTimeOffsetExtensions
    {
        internal static readonly DateTimeOffset Epoch = new DateTimeOffset(new DateTime(1970, 1, 1), TimeSpan.Zero);

        public static DateTimeOffset ToDateTimeOffset(long timestamp)
        {
            if (timestamp < 0)
                throw new ArgumentOutOfRangeException("timestamp");

            return Epoch.AddMilliseconds(timestamp);
        }

        public static long ToTimestamp(this DateTimeOffset dateTimeOffset)
        {
            if (dateTimeOffset < Epoch)
                throw new ArgumentOutOfRangeException("Cannot convert a time before the epoch (January 1, 1970, 00:00 UTC) to a timestamp.", "dateTimeOffset");

            return (long)(dateTimeOffset - Epoch).TotalMilliseconds;
        }

        public static DateTimeOffset? ToDateTimeOffset(long? timestamp)
        {
            if (timestamp == null)
                return null;

            return ToDateTimeOffset(timestamp.Value);
        }

        public static long? ToTimestamp(this DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset == null)
                return null;

            return ToTimestamp(dateTimeOffset.Value);
        }
    }
}
