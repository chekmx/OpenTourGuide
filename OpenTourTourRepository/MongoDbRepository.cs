using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using OpenTourInterfaces;
using OpenTourModel;

namespace OpenTourTourRepository
{
    public class MongoDbRepository : ITourRepository<Tour>
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<Tour> collection;
        public MongoDbRepository()
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("ToursDataBase");
            BsonClassMap.RegisterClassMap<Tour>(tour =>
            {
                tour.AutoMap();
                tour.MapCreator(t =>
                    new Tour(new Location(t.Center),
                    t.Route.ConvertAll(r => new Location(r)).ToList(),
                    t.PointsOfInterest.ConvertAll(p => new PointOfInterest(p)).ToList()));
            });

            BsonClassMap.RegisterClassMap<Location>(location =>
            {
                location.AutoMap();
            });

            BsonClassMap.RegisterClassMap<PointOfInterest>(poi =>
            {
                poi.AutoMap();
            });
            collection = database.GetCollection<Tour>("ToursCollection");
        }

        public IEnumerable<Tour> LoadAll()
        { 
            return collection.Find(new BsonDocument()).ToList();
        }

        public void Save(Tour tour)
        {
            var filter = Builders<Tour>.Filter.Eq("_id", tour.Id);
            var existingTour = collection.Find(filter).FirstOrDefault<Tour>();
            if (existingTour == null)
            {
                collection.InsertOne(tour);
            }
            else
            {
                collection.ReplaceOne(filter, tour);
            }
        }
    }
}
