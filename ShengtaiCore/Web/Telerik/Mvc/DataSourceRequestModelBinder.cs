using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik.Mvc
{
    public class DataSourceRequestModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            DataSourceRequest request = new DataSourceRequest
            {
                ServerPaging = this.GetServerPaging(bindingContext)
            };

            var logic = bindingContext.ValueProvider.GetValue("filter[logic]");
            if (logic != null && !string.IsNullOrEmpty(logic.FirstValue))
            {
                var filterInfoCollection = new ServerFilterInfo { Logic = this.ParseLogic(logic.FirstValue) };
                this.SetServerFiltering(bindingContext, filterInfoCollection, "filter[filters][{0}]", 0);

                if (filterInfoCollection.FilterCollection.Count > 0)
                    request.ServerFiltering = filterInfoCollection;
            }

            bindingContext.Result = ModelBindingResult.Success(request);
            return Task.CompletedTask;
        }

        private ServerPageInfo GetServerPaging(ModelBindingContext bindingContext)
        {
            var skip = bindingContext.ValueProvider.GetValue("skip");
            var take = bindingContext.ValueProvider.GetValue("take");
            if (skip != null && !string.IsNullOrEmpty(skip.FirstValue) && take != null && !string.IsNullOrEmpty(take.FirstValue))
            {
                var pageInfo = new ServerPageInfo
                {
                    Skip = Convert.ToInt32(skip.FirstValue),
                    Take = Convert.ToInt32(take.FirstValue)
                };

                var page = bindingContext.ValueProvider.GetValue("page");
                pageInfo.Page = string.IsNullOrEmpty(page.FirstValue) ? 1 : Convert.ToInt32(page.FirstValue);

                var pageSize = bindingContext.ValueProvider.GetValue("pageSize");
                pageInfo.PageSize = string.IsNullOrEmpty(pageSize.FirstValue) ? 1 : Convert.ToInt32(pageSize.FirstValue);

                return pageInfo;
            }

            return null;
        }

        private ServerFilterInfo SetServerFiltering(ModelBindingContext bindingContext, ServerFilterInfo filterInfoCollection, string format, int index)
        {
            string baseKey = string.Format(format, index++);
            var field = bindingContext.ValueProvider.GetValue(baseKey + "[field]"); // 用以判別單一 filter
            var subLogic = bindingContext.ValueProvider.GetValue(baseKey + "[logic]");  // 用以判別 filter 集合

            if (field != null && !string.IsNullOrEmpty(field.FirstValue))
            {
                var value = bindingContext.ValueProvider.GetValue(baseKey + "[value]");
                var @operator = bindingContext.ValueProvider.GetValue(baseKey + "[operator]");
                if (@operator != null && !string.IsNullOrEmpty(@operator.FirstValue))
                {
                    filterInfoCollection.FilterCollection.Add(new ServerFilterInfo
                    {
                        Operator = this.ParseOperator(@operator.FirstValue),
                        Field = field.FirstValue,
                        Value = value == null ? string.Empty : value.FirstValue
                    });

                    this.SetServerFiltering(bindingContext, filterInfoCollection, format, index);
                }
            }
            else if (subLogic != null && !string.IsNullOrEmpty(subLogic.FirstValue))
            {
                var subFilterInfoCollection = new ServerFilterInfo { Logic = this.ParseLogic(subLogic.FirstValue) };
                filterInfoCollection.FilterCollection.Add(this.SetServerFiltering(bindingContext, subFilterInfoCollection, baseKey + "[filters][{0}]", 0));
            }

            return filterInfoCollection;
        }

        protected FilterLogics ParseLogic(string logic)
        {
            switch (logic)
            {
                case "and":
                    return FilterLogics.And;

                case "or":
                    return FilterLogics.Or;

                default:
                    return FilterLogics.And;
            }
        }

        protected FilterOperations ParseOperator(string @operator)
        {
            switch (@operator)
            {
                //equal ==
                case "eq":
                case "==":
                case "isequalto":
                case "equals":
                case "equalto":
                case "equal":
                    return FilterOperations.Equals;
                //not equal !=
                case "neq":
                case "!=":
                case "isnotequalto":
                case "notequals":
                case "notequalto":
                case "notequal":
                case "ne":
                    return FilterOperations.NotEquals;
                // Greater
                case "gt":
                case ">":
                case "isgreaterthan":
                case "greaterthan":
                case "greater":
                    return FilterOperations.Greater;
                // Greater or equal
                case "gte":
                case ">=":
                case "isgreaterthanorequalto":
                case "greaterthanequal":
                case "ge":
                    return FilterOperations.GreaterOrEquals;
                // Less
                case "lt":
                case "<":
                case "islessthan":
                case "lessthan":
                case "less":
                    return FilterOperations.LessThan;
                // Less or equal
                case "lte":
                case "<=":
                case "islessthanorequalto":
                case "lessthanequal":
                case "le":
                    return FilterOperations.LessThanOrEquals;

                case "startswith":
                    return FilterOperations.StartsWith;

                case "endswith":
                    return FilterOperations.EndsWith;
                //string.Contains()
                case "contains":
                    return FilterOperations.Contains;

                case "doesnotcontain":
                    return FilterOperations.NotContains;

                default:
                    return FilterOperations.Contains;
            }
        }
    }
}
