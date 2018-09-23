using System.Collections.Generic;

namespace OpenTourClient.ViewModels
{
    public interface ITourRepository
    {
        List<TourViewModel> LoadAll();
    }
}