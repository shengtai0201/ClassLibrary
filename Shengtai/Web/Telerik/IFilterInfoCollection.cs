using System.Collections.Generic;

namespace Shengtai.Web.Telerik
{
    public interface IFilterInfoCollection
    {
        ICollection<ServerFilterInfo> FilterCollection { get; set; }

        /// <summary>
        /// 針對 Filters 裡所有的 FilterInfo 做邏輯判斷
        /// </summary>
        FilterLogics Logic { get; set; }
    }
}