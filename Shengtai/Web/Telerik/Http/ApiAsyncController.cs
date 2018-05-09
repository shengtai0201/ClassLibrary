using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(TKey key)
        {
            bool? result = await this.service.DestroyAsync(key);

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

        [HttpGet]
        public async Task<IDataSourceResponse<TModel>> Get([ModelBinder(typeof(DataSourceRequestModelBinder))]DataSourceRequest request)
        {
            return await this.service.ReadAsync(request);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.BadRequest, ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool result = await this.service.CreateAsync(model, response);

            if (result)
                return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.Created, response);
            else
                return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.InternalServerError, response);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Put(TKey key, TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.BadRequest, ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool? result = await this.service.UpdateAsync(key, model, response);

            if (result == null)
                return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.NotFound, response);
            else
            {
                if (result.Value)
                    return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.OK, response);
                else
                    return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.InternalServerError, response);
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