using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shengtai.Web.Mvc
{
    public class XmlResult : ActionResult
    {
        private readonly object model;
        public XmlResult(object model)
        {
            this.model = model;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (this.model == null)
                throw new ArgumentNullException();

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "text/xml";

            var serializer = new System.Xml.Serialization.XmlSerializer(this.model.GetType());
            serializer.Serialize(context.HttpContext.Response.Output, this.model);
        }
    }
}
