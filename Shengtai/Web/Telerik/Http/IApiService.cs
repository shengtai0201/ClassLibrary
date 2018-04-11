using System;

namespace Shengtai.Web.Telerik.Http
{
    public interface IApiService<TModel, TKey> : IService, IDisposable where TModel : class
    {
        IDataSourceResponse<TModel> Read(DataSourceRequest request);

        bool Create(TModel model, IDataSourceError error);

        bool? Update(TKey key, TModel model, IDataSourceError error);

        bool? Destroy(TKey key);
    }
}
