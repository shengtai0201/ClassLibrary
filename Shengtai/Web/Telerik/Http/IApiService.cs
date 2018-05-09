using System;

namespace Shengtai.Web.Telerik.Http
{
    public interface IApiService<TModel, TKey> : IService, IDisposable where TModel : class
    {
        bool Create(TModel model, IDataSourceError error);

        bool? Destroy(TKey key);

        IDataSourceResponse<TModel> Read(DataSourceRequest request);

        bool? Update(TKey key, TModel model, IDataSourceError error);
    }
}