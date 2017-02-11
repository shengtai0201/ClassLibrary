using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik
{
    public class ServerFilterInfo : IFilterInfo, IFilterInfoCollection
    {
        public string Field { get; set; }
        public FilterOperations Operator { get; set; }
        public string Value { get; set; }

        public ICollection<ServerFilterInfo> FilterCollection { get; set; }
        public FilterLogics Logic { get; set; }

        public ServerFilterInfo()
        {
            this.FilterCollection = new List<ServerFilterInfo>();
        }
    }
}
