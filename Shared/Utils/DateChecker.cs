using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utils
{
    public static class DateChecker
    {
        public static bool DateRangeIsInRange(DateTime firstRangeStart, DateTime? firstRangeEnd,
                                              DateTime secondRangeStart, DateTime? secondRangeEnd)
        {
            return (firstRangeStart < secondRangeStart &&
                    secondRangeStart < (firstRangeEnd ?? DateTime.MaxValue)) ||
                    (firstRangeStart < (secondRangeEnd ?? DateTime.MaxValue) &&
                    (secondRangeEnd ?? DateTime.MaxValue) <= (firstRangeEnd ?? DateTime.MaxValue)) ||
                    (secondRangeStart < firstRangeStart &&
                    firstRangeStart < (secondRangeEnd ?? DateTime.MaxValue)) ||
                    (secondRangeStart < (firstRangeEnd ?? DateTime.MaxValue) &&
                    (firstRangeEnd ?? DateTime.MaxValue) <= (secondRangeEnd ?? DateTime.MaxValue));
        }
    }
}
