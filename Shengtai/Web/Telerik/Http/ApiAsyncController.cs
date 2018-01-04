using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web;

namespace Shengtai.Web.Telerik.Http
{
    public abstract class ApiAsyncController<TModel, TKey> : ApiController where TModel : class
    {
        private readonly IApiAsyncService<TModel, TKey> service;

        protected ApiAsyncController(IApiAsyncService<TModel, TKey> service)
        {
            this.service = service;
            this.service.CurrentUser = this.User;
            this.service.OwinContext = HttpContext.Current.GetOwinContext();
        }

        [HttpGet]
        public async Task<DataSourceResponse<TModel>> Get([ModelBinder(typeof(DataSourceRequestModelBinder))]DataSourceRequest request)
        {
            return await this.service.Read(request);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.BadRequest, ModelState);

            IList<TModel> responseValue = new List<TModel>();
            bool result = await this.service.Create(model, responseValue);

            if (result)
                return Request.CreateResponse<TModel[]>(HttpStatusCode.Created, responseValue.ToArray());
            else
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.InternalServerError, ModelState);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Put(TKey key, TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.BadRequest, ModelState);

            IList<TModel> responseValue = new List<TModel>();
            bool? result = await this.service.Update(key, model, responseValue);

            if (result == null)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.NotFound, ModelState);
            else
            {
                if (result.Value)
                    return Request.CreateResponse<TModel[]>(HttpStatusCode.OK, responseValue.ToArray());
                else
                    return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.InternalServerError, ModelState);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(TKey key)
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
