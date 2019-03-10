using System;
using System.Collections.Generic;

namespace OpenTourInterfaces
{
    public interface ITourRepository<T> where T : ITour
    {
        IEnumerable<T> LoadAll();
        void Save(T tour);
    }
}