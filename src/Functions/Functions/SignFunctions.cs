using Functions.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Functions.Functions
{
    public class SignFunctions
    {
        private readonly IConfiguration _configuration;

        public SignFunctions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("SignIn")]
        public async Task SignIn([TimerTrigger("0 0 9 * * MON-FRI")]TimerInfo myTimer, ILogger log)
        {
            var authKey = _configuration["AuthToken"];
            var userId = _configuration["UserId"];

            await SignService.Sign(authKey, Convert.ToInt32(userId));
        }

        [FunctionName("SignOut")]
        public async Task SignOut([TimerTrigger("0 30 18 * * MON-FRI")]TimerInfo myTimer, ILogger log)
        {
            var authKey = _configuration["AuthToken"];
            var userId = _configuration["UserId"];

            await SignService.Sign(authKey, Convert.ToInt32(userId));
        }
    }
}