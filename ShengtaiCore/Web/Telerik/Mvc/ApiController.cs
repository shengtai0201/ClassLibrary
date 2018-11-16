using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik.Mvc
{
    /* 限制彈性以求簡化的設計
     * 1. 不繫結來自目前要求的路由資料，僅支援 "FromQuery" with "FromForm"
     * 2. StatusCode 參考 HttpStatusCode
     */
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public abstract class ApiController<TKey, TModel, TEntity> : ControllerBase where TModel : ViewModel<TKey, TModel, TEntity>
    {
        private readonly IApiService<TKey, TModel, TEntity> service;

        public ApiController(IApiService<TKey, TModel, TEntity> service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IDataSourceResponse<TModel>> GetAsync([ModelBinder] DataSourceRequest request)
        {
            return await this.service.ReadAsync(request);
        }

        [HttpGet("{key}")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync([FromQuery] TKey key)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TEntity entity = await this.service.ReadAsync(key);
            if (entity == null)
                return NotFound();
            else
            {
                var model = ViewModel<TKey, TModel, TEntity>.NewInstance(entity);
                return Ok(model);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] TKey key, [FromForm] TModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool? result = await this.service.UpdateAsync(key, model, response);

            if (result == null)
                return NotFound(response);
            else
            {
                if (result.Value)
                    return this.Ok(response);
                else
                    return this.StatusCode(500, response);
            }
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync([FromForm] TModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool result = await this.service.CreateAsync(model, response);

            if (result)
                return CreatedAtAction(nameof(GetAsync), new { key = model.GetKey() }, response);
            else
                return this.StatusCode(500, response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] TKey key)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool? result = await this.service.DestroyAsync(key);

            if (result == null)
                return this.NotFound();
            else
            {
                if (result.Value)
                    return Ok(new DataSourceResponse<TModel>());
                else
                    return this.StatusCode(500, new { key });
            }
        }
    }
}
