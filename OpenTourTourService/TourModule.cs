﻿using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using OpenTourClient.ViewModels;
using OpenTourModel;
using OpenTourInterfaces;

namespace OpenTourTourService
{
    public class TourModule : NancyModule
    {
        private static ITourRepository<Tour> repository = new FileTourRepository() as ITourRepository<Tour>;

        public TourModule()
        {
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