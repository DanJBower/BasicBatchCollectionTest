using System;
using System.Collections.Generic;
using System.Linq;
using School;

namespace SchoolEfCore.Entities
{
    public class Class : IClass
    {
        public Class()
        {
            ClassPupil = new HashSet<ClassPupil>();
        }

        public Guid Id { get; set; }

        public string Subject { get; set; }

        public ICollection<ClassPupil> ClassPupil { get; set; }

        public IEnumerable<IPupil> Pupils => ClassPupil.Select(_ => _.Pupil);
    }
}
