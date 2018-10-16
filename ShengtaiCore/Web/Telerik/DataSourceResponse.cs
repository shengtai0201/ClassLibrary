using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Web.Telerik
{
    public class DataSourceResponse<TModel> : IDataSource, IDataSourceResponse<TModel> where TModel : class
    {
        private readonly StringBuilder builder;

        public DataSourceResponse()
        {
            this.builder = new StringBuilder();
            this.DataCollection = new List<TModel>();
        }

        public ICollection<TModel> DataCollection { get; set; }

        public string ErrorMessage
        {
            get
            {
                return this.ToString();
            }
        }

        public int TotalRowCount { get; set; }

        public override string ToString()
        {
            return this.builder.ToString();
        }

        public StringBuilder AppendLine(string value)
        {
            return this.builder.AppendLine(value);
        }
    }
}
