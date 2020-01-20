using System;
using GraphQL.Types;
using GraphQL.Utilities;

namespace BasicCollectionLoaderTest.School
{
    public class SchoolSchema : Schema
    {
        public SchoolSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<SchoolQuery>();
        }
    }
}
