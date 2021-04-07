using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;

namespace Sample.Foundation.Search.Infrastructure
{
    public abstract class BaseComputedField : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public abstract object ComputeFieldValue(IIndexable indexable);
    }
}