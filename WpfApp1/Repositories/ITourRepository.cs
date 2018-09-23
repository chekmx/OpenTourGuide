using OpenTourClient.Models;
using System.Collections.Generic;

namespace OpenTourClient.ViewModels
{
    public interface ITourRepository
    {
        List<Tour> LoadAll();
    }
}