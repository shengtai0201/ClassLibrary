using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Web.Telerik
{
    public interface IDataSourceResponse<TModel> where TModel : class
    {
        ICollection<TModel> DataCollection { get; set; }

        string ErrorMessage { get; }
        int TotalRowCount { get; }
    }
}
