using Microsoft.AspNetCore.Mvc;
using Shengtai.Web.Telerik.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Web.Telerik.Mvc
{
    [ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))]
    public class DataSourceRequest
    {
        public IFilterInfoCollection ServerFiltering { get; set; }

        public ServerPageInfo ServerPaging { get; set; }
    }
}
