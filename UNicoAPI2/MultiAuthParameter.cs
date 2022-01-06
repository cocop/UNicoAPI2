using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNicoAPI2
{
    public class MultiAuthParameter
    {
        public string Code { get; set; }
        public string DeviceName { get; set; }
        public bool IsTrustedDevice { get; set; }
    }
}
