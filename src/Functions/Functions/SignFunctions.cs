using Functions.Service;
using Functions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Functions.Models;
using Newtonsoft.Json;

namespace Functions.Functions
{
    public class SignFunctions
    {
        private readonly IConfiguration _configuration;
        private readonly IWoffuToken _woffuToken;
        private string _user;
        private string _password;
        private string _token;

        public SignFunctions(IConfiguration configuration, IWoffuToken woffuToken)
        {
            _configuration = configuration;
            _woffuToken = woffuToken;
            _user = _configuration["user"];
            _password = _configuration["password"];
            _token = _woffuToken.GetToken(_user, _password);
        }

        [FunctionName("SignIn")]
        public async Task SignIn([TimerTrigger("0 0 9 * * MON-FRI")]TimerInfo myTimer, ILogger log)
        {
            var userId = _woffuToken.GetTokenToString(JsonConvert.DeserializeObject<JwtModel>(_token)).UserId;
            await SignService.Sign(_token, Convert.ToInt32(userId));
        }

        [FunctionName("SignOut")]
        public async Task SignOut([TimerTrigger("0 30 18 * * MON-FRI")]TimerInfo myTimer, ILogger log)
        {
            var userId = _woffuToken.GetTokenToString(JsonConvert.DeserializeObject<JwtModel>(_token)).UserId;
            await SignService.Sign(_token, Convert.ToInt32(userId));
        }

        [FunctionName("IsHolidayPost")]
        public async Task IsHolidayPost([HttpTrigger(AuthorizationLevel.Function, "get")]HttpRequest req, ILogger log, ExecutionContext context)
        {
            var userId = _woffuToken.GetTokenToString(JsonConvert.DeserializeObject<JwtModel>(_token)).UserId;
            var response = await DayService.IsHoliday(_token, Convert.ToInt32(userId));
            Console.WriteLine(response);
        }
    }
}