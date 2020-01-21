using System;
using SchoolEfCore.Context;
using SchoolEfCore.Entities;

namespace PopulateData
{
    internal static class Program
    {
        private static void Main()
        {
            Class science = MakeClassWithSubject("Science");
            Class technology = MakeClassWithSubject("Technology");
            Class engineering = MakeClassWithSubject("Engineering");
            Class maths = MakeClassWithSubject("Maths");

            Pupil phil = MakePupilWithName("Phil");
            Pupil joe = MakePupilWithName("Joe");
            Pupil mac = MakePupilWithName("Mac");
            Pupil rose = MakePupilWithName("Rose");
            Pupil harry = MakePupilWithName("Harry");
            Pupil meg = MakePupilWithName("meg");

            science.AddPupilTooClass(phil);
            science.AddPupilTooClass(meg);
            science.AddPupilTooClass(rose);

            technology.AddPupilTooClass(joe);
            technology.AddPupilTooClass(harry);
            technology.AddPupilTooClass(rose);

            engineering.AddPupilTooClass(joe);
            engineering.AddPupilTooClass(mac);
            engineering.AddPupilTooClass(harry);
            engineering.AddPupilTooClass(phil);
            engineering.AddPupilTooClass(rose);

            maths.AddPupilTooClass(meg);
            maths.AddPupilTooClass(mac);
            maths.AddPupilTooClass(joe);

            SchoolContext context = new SchoolContext(SchoolContext.DefaultOptions.Options);

            context.Pupils.Add(phil);
            context.Pupils.Add(joe);
            context.Pupils.Add(mac);
            context.Pupils.Add(rose);
            context.Pupils.Add(harry);
            context.Pupils.Add(meg);

            context.Classes.Add(science);
            context.Classes.Add(technology);
            context.Classes.Add(engineering);
            context.Classes.Add(maths);

            context.SaveChanges();
        }

        private static Class MakeClassWithSubject(string subject) => new Class
        {
            Id = Guid.NewGuid(),
            Subject = subject
        };

        private static Pupil MakePupilWithName(string name) => new Pupil
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        private static void AddPupilTooClass(this Class @class, Pupil pupil)
        {
            @class.ClassPupil.Add(new ClassPupil
            {
                ClassId = @class.Id,
                PupilId = pupil.Id
            });
        }
    }
}