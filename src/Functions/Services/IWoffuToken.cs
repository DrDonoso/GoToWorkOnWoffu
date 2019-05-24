using Functions.Configuration;
using Functions.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Functions.Services
{
    public interface IWoffuToken
    {
        string GetToken(string user, string password);
        BearerModel GetTokenToString(JwtModel tokenIn);
    }

    public class WoffuToken : IWoffuToken
    {
        private readonly TokenOptions _configuration;

        public WoffuToken(IOptions<TokenOptions> configuration)
        {
            _configuration = configuration.Value;
        }

        public string GetToken(string user, string password)
        {
            var client = new RestClient($"https://app.woffu.com/Token");
            var request = new RestRequest(Method.POST);
            request.AddParameter("undefined", $"grant_type=password&username={_configuration.User}&password={_configuration.Password}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        public BearerModel GetTokenToString(JwtModel tokenIn)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            try
            {
                var token = jwtHandler.ReadJwtToken(tokenIn.access_token);

                var headers = token.Header;
                var jwtHeader = "{";
                foreach (var (key, value) in headers)
                {
                    jwtHeader += '"' + key + "\":\"" + value + "\",";
                }
                jwtHeader += "}";

                var claims = token.Claims;
                var jwtPayload = "{";
                foreach (Claim c in claims)
                {
                    jwtPayload += '"' + c.Type + "\":\"" + c.Value + "\",";
                }
                jwtPayload += "}";
                return JsonConvert.DeserializeObject<BearerModel>(jwtPayload);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}