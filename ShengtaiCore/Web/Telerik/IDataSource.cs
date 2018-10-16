using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Web.Telerik
{
    public interface IDataSource
    {
        string ToString();

        StringBuilder AppendLine(string value);
    }
}
