using System;

namespace PagerDutyStats
{
    public class DateRange
    {
        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public override string ToString()
        {
            return DateFunctions.AsIso8601ToMinute(Start) + " to " + DateFunctions.AsIso8601ToMinute(End);
        }
    }
}