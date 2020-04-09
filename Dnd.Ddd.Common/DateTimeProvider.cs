using System;

namespace Dnd.Ddd.Common
{
    public static class DateTimeProvider
    {
        private static readonly Func<DateTime> DateTimeNowDefaultImplementation = () => DateTime.Now;

        private static readonly Func<DateTime> DateTimeUtcNowDefaultImplementation = () => DateTime.UtcNow;

        private static Func<DateTime> nowImplementation = DateTimeNowDefaultImplementation;

        private static Func<DateTime> utcNowImplementation = DateTimeUtcNowDefaultImplementation;

        public static DateTime Now => nowImplementation();

        public static DateTime UtcNow => utcNowImplementation();

        public static void SetNowImplementation(Func<DateTime> dateTimeNowImplementation) => nowImplementation = dateTimeNowImplementation;

        public static void SetUtcNowImplementation(Func<DateTime> dateTimeUtcNowImplementation) =>
            utcNowImplementation = dateTimeUtcNowImplementation;

        public static void ResetImplementations()
        {
            nowImplementation = DateTimeNowDefaultImplementation;
            utcNowImplementation = DateTimeUtcNowDefaultImplementation;
        }
    }
}