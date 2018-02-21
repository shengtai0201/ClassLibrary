using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik.Http
{
    public interface IApiAsyncService<TModel, TKey> : IService, IDisposable where TModel : class
    {
        Task<DataSourceResponse<TModel>> ReadAsync(DataSourceRequest request);

        Task<bool> CreateAsync(TModel model, IDataSourceError error);

        Task<bool?> UpdateAsync(TKey key, TModel model, IDataSourceError error);

        Task<bool?> DestroyAsync(TKey key);
    }
}
