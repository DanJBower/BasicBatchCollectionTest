using System;
using System.Collections.Generic;

namespace School
{
    public interface IClass
    {
        Guid Id { get; }

        string Subject { get; }

        IEnumerable<IPupil> Pupils { get; }
    }
}
