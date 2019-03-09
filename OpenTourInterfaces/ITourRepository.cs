using OpenTourInterfaces;
using System.Collections.Generic;

namespace OpenTourClient.ViewModels
{
    public interface ITourRepository<T> where T : ITour
    {
        IEnumerable<T> LoadAll();
        void Save(T tour);
    }
}