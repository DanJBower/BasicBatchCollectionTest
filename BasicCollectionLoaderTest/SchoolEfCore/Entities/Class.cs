using System;
using System.Collections.Generic;

namespace SchoolEfCore.Entities
{
    public class Class
    {
        public Class()
        {
            ClassPupil = new HashSet<ClassPupil>();
        }

        public Guid Id { get; set; }

        public string Subject { get; set; }

        public ICollection<ClassPupil> ClassPupil { get; set; }
    }
}
