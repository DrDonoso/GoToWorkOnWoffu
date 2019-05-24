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
using Functions.Configuration;
using Microsoft.Extensions.Options;

namespace Functions.Functions
{
    public class SignFunctions
    {
        private TokenOptions _configuration;
        private IWoffuToken _woffuToken;
        private IWoffuServices _woffuServices;
        private string _user;
        private string _password;
        private string _token;
        private JwtModel _jwtToken;
        private BearerModel _bearer;

        public SignFunctions(IOptions<TokenOptions> configuration, IWoffuToken woffuToken, IWoffuServices woffuServices)
        {
            SetUp(configuration.Value, woffuToken, woffuServices);
        }

        [FunctionName("SignIn")]
        public void SignIn([TimerTrigger("0 0 9 * * MON-FRI")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            if (!_woffuServices.IsHoliday(_bearer.UserId, _jwtToken.access_token))
            {
                _woffuServices.Sign(Convert.ToInt32(_bearer.UserId), _jwtToken.access_token);
            }
        }

        [FunctionName("SignOut")]
        public void SignOut([TimerTrigger("0 30 18 * * MON-FRI")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            if (!_woffuServices.IsHoliday(_bearer.UserId, _jwtToken.access_token))
            {
                 _woffuServices.Sign(Convert.ToInt32(_bearer.UserId), _jwtToken.access_token);
            }
        }

        [FunctionName("IsHolidayPost")]
        public void IsHolidayPost([HttpTrigger(AuthorizationLevel.Function, "get")]HttpRequest req, ILogger log, ExecutionContext context)
        {
            var result = _woffuServices.IsHoliday(_bearer.UserId, _jwtToken.access_token);
        }

        private void SetUp(TokenOptions configuration, IWoffuToken woffuToken, IWoffuServices woffuServices)
        {
            _configuration = configuration;
            _woffuToken = woffuToken;
            _woffuServices = woffuServices;
            _user = configuration.User;
            _password = configuration.Password;
            _token = _woffuToken.GetToken(_user, _password);
            _jwtToken = JsonConvert.DeserializeObject<JwtModel>(_token);
            _bearer = _woffuToken.GetTokenToString(JsonConvert.DeserializeObject<JwtModel>(_token));
        }
    }
}