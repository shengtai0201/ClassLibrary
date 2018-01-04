using System;

namespace Shengtai.Web.Telerik.Http
{
    public interface IApiService<TModel, TKey> : IController, IDisposable where TModel : class
    {
        DataSourceResponse<TModel> Read(DataSourceRequest request);

        bool Create(TModel model, IDataSourceError error);

        bool? Update(TKey key, TModel model, IDataSourceError error);

        bool? Destroy(TKey key);
    }
}
