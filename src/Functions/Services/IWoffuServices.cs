using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Functions.Services
{
    public interface IWoffuServices
    {
        string Sign(int userId, string bearer);
        bool IsHoliday(int userId, string bearer);
    }

    public class WoffuServices : IWoffuServices
    {
        public string Sign(int userId, string bearer)
        {
            var client = new RestClient($"https://plainconcepts.woffu.com/api/signs");

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {bearer}");
            IRestResponse response = client.Execute(request);

            return response.Content ;
        }

        public bool IsHoliday(int userId, string bearer)
        {
            var client = new RestClient($"https://plainconcepts.woffu.com/api/users/{userId}/requests?pageSize=50&statusId=20");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {bearer}");
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<IEnumerable<DayInfo>>(response.Content).Any(x => x.StartDate.Date == DateTime.Today.Date);
        }
    }
}