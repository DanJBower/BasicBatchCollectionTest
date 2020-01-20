using GraphQL;

namespace BasicCollectionLoaderTest.GraphQlConfig
{
    public class GraphQlQuery
    {
        public string OperationName { get; set; }

        public string Query { get; set; }

        public Inputs Variables { get; set; }
    }
}
