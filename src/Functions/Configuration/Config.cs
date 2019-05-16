using Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(Functions.Configuration.Config))]
namespace Functions.Configuration
{
    public class Config : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            services.AddScoped<IWoffuToken, WoffuToken>();
        }
    }
}