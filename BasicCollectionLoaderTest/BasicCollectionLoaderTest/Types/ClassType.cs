//using Boerman.GraphQL.Contrib.DataLoaders;
using BasicCollectionLoaderTest.SourceCopy;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using SchoolEfCore.Entities;
using SchoolEfCore.Interfaces;
using System;
using System.Linq;

namespace BasicCollectionLoaderTest.Types
{
    public sealed class ClassType : ObjectGraphType<Class>
    {
        public ClassType(IDataLoaderContextAccessor accessor, ISchoolContext schoolContext)
        {
            Field(_ => _.Id, type: typeof(IdGraphType))
                .Description("The class' ID.");

            Field(_ => _.Subject)
                .Description("The class' subject.");

            Field(_ => _.Pupils, type: typeof(ListGraphType<PupilType>))
                .Description("The class' pupils.")
                .ResolveAsync(
                    async context =>
                    {
                        var pupils = await accessor.EntityCollectionLoader(
                            () => schoolContext.ClassPupil.Include(q => q.Pupil),
                            q => q.ClassId,
                            q => q,
                            q => q.ClassId,
                            context.Source.Id);

                        return pupils.Select(q => q.Pupil);
                    });
        }
    }
}
