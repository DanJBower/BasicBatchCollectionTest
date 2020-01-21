using GraphQL.Types;
using SchoolEfCore.Entities;

namespace BasicCollectionLoaderTest.Types
{
    public sealed class PupilType : ObjectGraphType<Pupil>
    {
        public PupilType()
        {
            Field(_ => _.Id, type: typeof(IdGraphType))
                .Description("The pupil's ID.");

            Field(_ => _.Name)
                .Description("The pupil's subject.");
        }
    }
}
