using Microsoft.EntityFrameworkCore;
using SchoolEfCore.Entities;

namespace SchoolEfCore.Interfaces
{
    public interface ISchoolContext : IDbContext
    {
        DbSet<Class> Classes { get; set; }

        DbSet<ClassPupil> ClassPupil { get; set; }

        DbSet<Pupil> Pupils { get; set; }
    }
}
