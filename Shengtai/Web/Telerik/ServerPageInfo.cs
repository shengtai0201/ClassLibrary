namespace Shengtai.Web.Telerik
{
    public class ServerPageInfo
    {
        /// <summary>
        /// the page of data item to return (1 means the first page)
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// the number of items to return
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// how many data items to skip
        /// 可以用於 SQL's BETWEEN @Begin AND @End，@Begin = Skip + 1
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// the number of data items to return (the same as pageSize)
        /// 可以用於 SQL's BETWEEN @Begin AND @End，@End = Skip + Take
        /// </summary>
        public int Take { get; set; }
    }
}