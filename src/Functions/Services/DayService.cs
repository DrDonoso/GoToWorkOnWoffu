using System;
using System.Collections.Generic;
using System.Linq;
using Functions.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Functions.Service
{
    public static class DayService
    {
        public static async Task<bool> IsHoliday(string authToken, int userId)
        {
            var uri = $"https://plainconcepts.woffu.com/api/users/{userId}/requests?pageSize=50&statusId=20";

            var httpClient = new HttpClient();
            var message = new HttpRequestMessage(HttpMethod.Get, uri);
            var body = JsonConvert.SerializeObject(new Message(userId));

            message.Content = new StringContent(body, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
            var response = await httpClient.SendAsync(message);

            var dayInfo = JsonConvert.DeserializeObject<IEnumerable<DayInfo>>(response.Content.ReadAsStringAsync().Result);

            return dayInfo.Any(x => x.StartDate.Date == DateTime.Today.Date);
        }
    }
}
