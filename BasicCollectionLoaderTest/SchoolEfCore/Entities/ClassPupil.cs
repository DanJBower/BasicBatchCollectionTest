using System;

namespace SchoolEfCore.Entities
{
    public class ClassPupil
    {
        public Guid PupilId { get; set; }
        public Pupil Pupil { get; set; }

        public Guid ClassId { get; set; }
        public Class Class { get; set; }
    }
}
