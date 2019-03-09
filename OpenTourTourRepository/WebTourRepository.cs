using System.Collections.Generic;
using OpenTourInterfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace OpenTourClient.ViewModels
{
    public class WebTourRepository<T> : ITourRepository<T> where T : ITour
    {
        public WebTourRepository()  {}

        private static readonly HttpClient client = new HttpClient();
        private List<T> tours;

        public IEnumerable<T> LoadAll()
        {
            string responce = client.GetStringAsync("http://localhost:61010/").GetAwaiter().GetResult();
            this.tours = JsonConvert.DeserializeObject<List<T>>(responce); ;
            return this.tours;
        }

        public void Save(T tour)
        {
            var content = new StringContent(JsonConvert.SerializeObject(tour), Encoding.UTF8, "application/json");
            var response = client.PostAsync("http://localhost:61010/saveTour", content).GetAwaiter().GetResult();
            this.tours.Add(tour);
        }

    }
}