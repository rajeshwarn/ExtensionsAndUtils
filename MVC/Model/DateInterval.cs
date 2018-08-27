using System;

namespace Shared.Utils
{
    public class DateInterval : IInterval<DateTime>
    {
        protected DateInterval(){ }

        public DateInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; protected set; }
        public DateTime End { get; protected set; }

        public virtual bool Includes(DateTime value)
        {
            return (Start <= value) && (value <= End);
        }

        public virtual bool Includes(IInterval<DateTime> interval)
        {
            return (Start <= interval.Start) && (interval.End <= End);
        }

        public bool Before(DateTime value)
        {
            return value > End;
        }

        public bool Before(IInterval<DateTime> interval)
        {
            return interval.Start > End;
        }

        public bool After(DateTime value)
        {
            return value < Start;
        }

        public bool After(IInterval<DateTime> interval)
        {
            return interval.End < Start;
        }
    }
}