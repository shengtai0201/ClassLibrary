using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik.Mvc
{
    public interface IApiService<TKey, TModel, TEntity> where TModel : ViewModel<TKey, TModel, TEntity>
    {
        Task<bool> CreateAsync(TModel model, IDataSource dataSource);

        Task<TEntity> ReadAsync(TKey key);
        Task<IDataSourceResponse<TModel>> ReadAsync(DataSourceRequest request);

        Task<bool?> UpdateAsync(TKey key, TModel model, IDataSource dataSource);

        Task<bool?> DestroyAsync(TKey key);
    }
}
