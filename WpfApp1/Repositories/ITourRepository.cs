using OpenTourInterfaces;
using System.Collections.Generic;

namespace OpenTourClient.ViewModels
{
    public interface ITourRepository
    {
        IEnumerable<ITour> LoadAll();
        void Save(ITour tour);
    }
}