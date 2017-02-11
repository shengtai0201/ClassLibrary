using System;

namespace Shengtai.Web.Telerik.Http
{
    public interface IApiService<TModel, TKey> : ICurrentUser, IDisposable where TModel : class
    {
        DataSourceResponse<TModel> Read(DataSourceRequest request);

        bool Create(TModel model, DataSourceResponse<TModel> response);

        bool? Update(TKey key, TModel model, DataSourceResponse<TModel> response);

        bool? Destroy(TKey key);
    }
}
