using Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


[assembly: FunctionsStartup(typeof(Functions.Configuration.Config))]
namespace Functions.Configuration
{
    public class Config : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;

            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddScoped<IWoffuToken, WoffuToken>();
            services.AddScoped<IWoffuServices, WoffuServices>();
            //services.AddSingleton<IConfiguration>();

            services.Configure<TokenOptions>(config);
        }
    }
}