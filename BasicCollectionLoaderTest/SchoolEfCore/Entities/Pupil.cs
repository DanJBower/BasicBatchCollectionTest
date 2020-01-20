using System;
using System.Collections.Generic;

namespace SchoolEfCore.Entities
{
    public class Pupil
    {
        public Pupil()
        {
            ClassPupil = new HashSet<ClassPupil>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<ClassPupil> ClassPupil { get; set; }
    }
}
