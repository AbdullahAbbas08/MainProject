namespace DomainLayer.Helpers.Extension
{
    public static class DateTimeExtensions
    {
        public static string ToEgyptDateTimeString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        public static string ToEgyptDateString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
    }
}
