using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public interface IAppSettings
    {
        string DefaultConnection { get; set; }
        string DateTimeOffsetFormat { get; set; }
    }
}
