using System.Collections.Generic;
using System.Security.Claims;

namespace BasicCollectionLoaderTest.GraphQlConfig
{
    public class GraphQlUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
