using System.Linq;
using BasicCollectionLoaderTest.Types;
using GraphQL.Types;
using SchoolEfCore.Interfaces;

namespace BasicCollectionLoaderTest.School
{
    public class SchoolQuery : ObjectGraphType
    {
        private const string Pupils = "pupils";
        private const string Classes = "classes";

        public SchoolQuery(ISchoolContext schoolContext)
        {
            Field<ListGraphType<PupilType>>(
                Pupils,
                $"How to query {Pupils}.",
                new QueryArguments(),
                context => schoolContext.Pupils.ToList());

            Field<ListGraphType<ClassType>>(
                Classes,
                $"How to query {Classes}.",
                new QueryArguments(),
                context => schoolContext.Classes.ToList());
        }
    }
}
