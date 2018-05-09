using System;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik.Http
{
    public interface IApiAsyncService<TModel, TKey> : IService, IDisposable where TModel : class
    {
        Task<bool> CreateAsync(TModel model, IDataSourceError error);

        Task<bool?> DestroyAsync(TKey key);

        Task<IDataSourceResponse<TModel>> ReadAsync(DataSourceRequest request);

        Task<bool?> UpdateAsync(TKey key, TModel model, IDataSourceError error);
    }
}