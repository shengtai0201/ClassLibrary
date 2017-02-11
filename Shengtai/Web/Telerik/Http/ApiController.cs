using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Shengtai.Web.Telerik.Http
{
    public abstract class ApiController<TModel, TKey> : ApiController where TModel : class
    {
        private readonly IApiService<TModel, TKey> service;

        protected ApiController(IApiService<TModel, TKey> service)
        {
            this.service = service;
            this.service.CurrentUser = this.User;
        }

        [HttpGet]
        public DataSourceResponse<TModel> Get([ModelBinder(typeof(DataSourceRequestModelBinder))]DataSourceRequest request)
        {
            return this.service.Read(request);
        }

        [HttpPost]
        public HttpResponseMessage Post(TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.BadRequest, ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool result = this.service.Create(model, response);

            if (result)
                return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.Created, response);
            else
                return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.InternalServerError, response);
        }

        [HttpPut]
        public HttpResponseMessage Put(TKey key, TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse<ModelStateDictionary>(HttpStatusCode.BadRequest, ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool? result = this.service.Update(key, model, response);

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

        [HttpDelete]
        public IHttpActionResult Delete(TKey key)
        {
            bool? result = this.service.Destroy(key);

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
