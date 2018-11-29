using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Options
{
    public class LogLevel
    {
        public string Default { get; set; } = "Trace";
        public string System { get; set; }
        public string Microsoft { get; set; }
    }

    public class Console
    {
        public bool IncludeScopes { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
        public Console Console { get; set; }
    }
}
