using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Web.Telerik
{
    public class ServerFilterInfo : IFilterInfo, IFilterInfoCollection
    {
        public ServerFilterInfo()
        {
            this.FilterCollection = new List<ServerFilterInfo>();
        }

        public string Field { get; set; }
        public ICollection<ServerFilterInfo> FilterCollection { get; set; }
        public FilterLogics Logic { get; set; }
        public FilterOperations Operator { get; set; }
        public string Value { get; set; }
    }
}
