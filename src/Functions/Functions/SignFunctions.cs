using Functions.Models;
using Functions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Functions.Functions
{
    public class SignFunctions
    {
        private IConfiguration _configuration;
        private IWoffuToken _woffuToken;
        private IWoffuServices _woffuServices;
        private string _user;
        private string _password;
        private string _token;
        private JwtModel _jwtToken;
        private BearerModel _bearer;

        public SignFunctions(IConfiguration configuration, IWoffuToken woffuToken, IWoffuServices woffuServices)
        {
            SetUp(configuration, woffuToken, woffuServices);
        }

        [FunctionName("SignIn")]
        public async Task SignIn([TimerTrigger("0 0 9 * * MON-FRI")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            if (!_woffuServices.IsHoliday(_bearer.UserId, _jwtToken.access_token))
            {
                await _woffuServices.Sign(Convert.ToInt32(_bearer.UserId), _jwtToken.access_token);
            }
        }

        [FunctionName("SignOut")]
        public async Task SignOut([TimerTrigger("0 30 18 * * MON-FRI")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            if (!_woffuServices.IsHoliday(_bearer.UserId, _jwtToken.access_token))
            {
                await _woffuServices.Sign(Convert.ToInt32(_bearer.UserId), _jwtToken.access_token);
            }
        }

        [FunctionName("IsHolidayPost")]
        public async Task IsHolidayPost([HttpTrigger(AuthorizationLevel.Function, "get")]HttpRequest req, ILogger log, ExecutionContext context)
        {
            var result = _woffuServices.IsHoliday(_bearer.UserId, _jwtToken.access_token);
        }

        private void SetUp(IConfiguration configuration, IWoffuToken woffuToken, IWoffuServices woffuServices)
        {
            _configuration = configuration;
            _woffuToken = woffuToken;
            _woffuServices = woffuServices;
            _user = _configuration["user"];
            _password = _configuration["password"];
            _token = _woffuToken.GetToken(_user, _password);
            _jwtToken = JsonConvert.DeserializeObject<JwtModel>(_token);
            _bearer = _woffuToken.GetTokenToString(JsonConvert.DeserializeObject<JwtModel>(_token));
        }
    }
}