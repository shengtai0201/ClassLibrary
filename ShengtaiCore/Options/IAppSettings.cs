using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Options
{
    public interface IAppSettings<T> where T : IDefaultConnection
    {
        T ConnectionStrings { get; set; }
        string DateTimeOffsetFormat { get; set; }
    }
}
