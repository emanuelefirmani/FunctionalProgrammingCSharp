using System;
using LaYumba.Functional;
using Enum = LaYumba.Functional.Enum;

namespace HonestFunctions
{
    public class DayOfWeekHelper
    {
        public static Option<DayOfWeek> GetDay(string input)
        {
            return Enum.Parse<DayOfWeek>(input);
        }
    }
}    