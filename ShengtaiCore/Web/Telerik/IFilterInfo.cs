using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Web.Telerik
{
    public interface IFilterInfo
    {
        string Field { get; }

        FilterOperations Operator { get; }

        string Value { get; }
    }
}
