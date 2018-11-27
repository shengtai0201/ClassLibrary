using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Options
{
    public class LogLevel
    {
        public string Default { get; set; } = "Trace";
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }
}
