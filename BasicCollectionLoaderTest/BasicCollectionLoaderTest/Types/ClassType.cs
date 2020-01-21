using GraphQL.Types;
using SchoolEfCore.Entities;

namespace BasicCollectionLoaderTest.Types
{
    public sealed class ClassType : ObjectGraphType<Class>
    {
        public ClassType()
        {
            Field(_ => _.Id, type: typeof(IdGraphType))
                .Description("The class' ID.");

            Field(_ => _.Subject)
                .Description("The class' subject.");
        }
    }
}
