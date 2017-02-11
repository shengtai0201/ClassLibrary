namespace Shengtai.Web.Telerik
{
    public interface IFilterInfo
    {
        string Field { get; }

        FilterOperations Operator { get; }

        string Value { get; }
    }
}
