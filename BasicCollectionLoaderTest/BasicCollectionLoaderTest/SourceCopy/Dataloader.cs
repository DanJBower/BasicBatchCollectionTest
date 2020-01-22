using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boerman.GraphQL.Contrib.DataLoaders;
using GraphQL.DataLoader;

namespace BasicCollectionLoaderTest.SourceCopy
{
    public static class Dataloader
    {
        public static async Task<IEnumerable<TSelect>> EntityCollectionLoader<T, TValue, TSelect>(
            this IDataLoaderContextAccessor dataLoader,
            Func<IQueryable<T>> queryableAccessor,
            Expression<Func<T, TValue>> dataPredicate,
            Expression<Func<T, TSelect>> selector,
            Expression<Func<TSelect, TValue>> outputPredicate,
            TValue value)
            where T : class
        {
            if (value == null) return default;

            var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<TValue, TSelect>(
                $"{typeof(T).Name}-{dataPredicate}",
                async items =>
                {
                    var queryable = queryableAccessor.Invoke();

                    var result = queryable
                        .Where(items.ToList().MatchOn(dataPredicate))
                        .Select(selector)
                        .ToList()
                        .ToLookup(outputPredicate.Compile());

                    return result;
                });

            var task = loader.LoadAsync(value);
            return await task;
        }
    }
}