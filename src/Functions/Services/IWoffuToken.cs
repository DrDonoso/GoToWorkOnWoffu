using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Functions.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Functions.Services
{
    public interface IWoffuToken
    {
        string GetToken(string user, string password);
        BearerModel GetTokenToString(JwtModel tokenIn);
    }

    public class WoffuToken : IWoffuToken
    {
        private readonly IConfiguration _configuration;

        public WoffuToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken(string user, string password)
        {
            var client = new RestClient($"https://app.woffu.com/Token");
            var request = new RestRequest(Method.POST);
            request.AddParameter("undefined", $"grant_type=password&username={_configuration["user"]}&password={_configuration["password"]}", ParameterType.RequestBody);
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