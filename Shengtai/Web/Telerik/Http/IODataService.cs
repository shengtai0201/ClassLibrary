using System;
using System.Threading.Tasks;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace Shengtai.Web.Telerik.Http
{
    public interface IODataService<TModel, TKey> : IDisposable where TModel : class
    {
        Task<PageResult<TModel>> Read(ODataQueryOptions<TModel> options);

        Task<bool?> Update(TKey key, TModel model);

        Task<bool> Create(TModel model);

        Task<bool?> Destroy(TKey key);
    }
}
