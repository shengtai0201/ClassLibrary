using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace Shengtai.Web.Telerik.Http
{
    public abstract class ODataController<TModel, TKey> : ODataController where TModel : class
    {
        private readonly IODataService<TModel, TKey> service;

        protected ODataController(IODataService<TModel, TKey> service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<PageResult<TModel>> Get(ODataQueryOptions<TModel> options)
        {
            return await this.service.Read(options);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromODataUri] TKey key, TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.BadRequest, ModelState);

            bool? result = await this.service.Update(key, model);

            if (result == null)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.NotFound, ModelState);
            else
            {
                if (result.Value)
                    return Request.CreateResponse<TModel[]>(HttpStatusCode.OK, new[] { model });
                else
                    return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.InternalServerError, ModelState);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.BadRequest, ModelState);

            bool result = await this.service.Create(model);

            if (result)
                return Request.CreateResponse<TModel[]>(HttpStatusCode.Created, new[] { model });
            else
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.InternalServerError, ModelState);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] TKey key)
        {
            bool? result = await this.service.Destroy(key);

            if (result == null)
                return this.NotFound();
            else
            {
                if (result.Value)
                    return this.StatusCode(HttpStatusCode.NoContent);
                else
                    return this.InternalServerError();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this.service.Dispose();

            base.Dispose(disposing);
        }
    }
}
