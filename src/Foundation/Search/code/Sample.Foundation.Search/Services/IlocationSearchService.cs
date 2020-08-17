using Sample.Foundation.Search.Models;
using System;
using System.Linq.Expressions;

namespace Sample.Foundation.Search.Services
{
    public interface ILocationSearchService<T>
    {
        SearchResults<T> SearchWithLocation(LocationSearchQuery query, Expression<Func<T, bool>> predicate);
    }
}
