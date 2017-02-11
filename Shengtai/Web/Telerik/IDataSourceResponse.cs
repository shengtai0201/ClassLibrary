using System.Collections.Generic;

namespace Shengtai.Web.Telerik
{
    public interface IDataSourceResponse<TModel> where TModel : class
    {
        ICollection<TModel> DataCollection { get; }

        int TotalRowCount { get; }

        string ErrorMessage { get; }
    }
}
