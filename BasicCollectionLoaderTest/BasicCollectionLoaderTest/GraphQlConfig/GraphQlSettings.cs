using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BasicCollectionLoaderTest.GraphQlConfig
{
    public class GraphQlSettings
    {
        public PathString Path { get; set; }

        public Func<HttpContext, IDictionary<string, object>> BuildUserContext { get; set; }

        public bool EnableMetrics { get; set; }
    }
}
