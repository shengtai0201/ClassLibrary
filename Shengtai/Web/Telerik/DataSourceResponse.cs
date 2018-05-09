using System.Collections.Generic;
using System.Text;

namespace Shengtai.Web.Telerik
{
    public class DataSourceResponse<TModel> : IDataSourceResponse<TModel>, IDataSourceError where TModel : class
    {
        public DataSourceResponse()
        {
            this.Error = new StringBuilder();
            this.DataCollection = new List<TModel>();
        }

        public ICollection<TModel> DataCollection { get; set; }

        public StringBuilder Error { get; set; }

        public string ErrorMessage
        {
            get
            {
                return this.Error.ToString();
            }
        }

        public int TotalRowCount { get; set; }
    }
}