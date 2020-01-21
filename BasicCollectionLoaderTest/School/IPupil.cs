using System;

namespace School
{
    public interface IPupil
    {
        Guid Id { get; }

        string Name { get; }
    }
}
