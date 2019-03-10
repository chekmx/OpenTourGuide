using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using OpenTourTourRepository;
using OpenTourModel;
using OpenTourInterfaces;
using Nancy.Json;
using System;

namespace OpenTourTourService
{
    public class TourModule : NancyModule
    {
        private static ITourRepository<Tour> repository = new MongoDbRepository() as ITourRepository<Tour>;

        public TourModule()
        {
            JsonSettings.MaxJsonLength = Int32.MaxValue; 
            Get["/"] = GetAll;
            Get["/tours"] = GetAll;
            Post["/saveTour"] = Save;
        }

        private Response Save(dynamic o)
        {
            repository.Save(JsonConvert.DeserializeObject<Tour>(Request.Body.AsString()));
            return HttpStatusCode.OK;
        }

        private Response GetAll(dynamic o)
        {
            return Response.AsJson(repository.LoadAll());
        }
    }
}