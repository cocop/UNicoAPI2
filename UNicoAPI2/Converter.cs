using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNicoAPI2
{
    public static class Converter
    {
        public static TimeSpan ToTimeSpan(string TimeSpan)
        {
            string[] buf = TimeSpan.Split(':');
            var minute = int.Parse(buf[0]);

            return new TimeSpan((int)(minute / 60), minute % 60, int.Parse(buf[1]));
        }
    }
}
