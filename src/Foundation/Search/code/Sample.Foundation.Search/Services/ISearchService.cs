using Sample.Foundation.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sample.Foundation.Search.Services
{
    public interface ISearchService<T>
    {
        SearchResults<T> Search(Query query, Expression<Func<T, bool>> predicate);
    }
}
