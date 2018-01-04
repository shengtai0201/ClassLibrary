using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik.Http
{
    public interface IApiAsyncService<TModel, TKey> : IController, IDisposable where TModel : class
    {
        Task<DataSourceResponse<TModel>> Read(DataSourceRequest request);

        Task<bool> Create(TModel model, IList<TModel> responseValue);

        Task<bool?> Update(TKey key, TModel model, IList<TModel> responseValue);

        Task<bool?> Destroy(TKey key);
    }
}
