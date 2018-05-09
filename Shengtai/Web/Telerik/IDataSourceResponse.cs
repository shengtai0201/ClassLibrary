using System.Collections.Generic;

namespace Shengtai.Web.Telerik
{
    public interface IDataSourceResponse<TModel> where TModel : class
    {
        ICollection<TModel> DataCollection { get; }

        string ErrorMessage { get; }
        int TotalRowCount { get; }
    }
}