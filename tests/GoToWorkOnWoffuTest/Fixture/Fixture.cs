using Functions.Configuration;
using Functions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GoToWorkOnWoffuTest.Fixture
{

    public class Fixture
    {
        public Fixture()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton<IOptions<TokenOptions>>(new OptionsWrapper<TokenOptions>(new TokenOptions()
            {
                User = "USUARIO",
                Password = "PASSWORD"
            }));

            serviceCollection.AddScoped<IWoffuToken, WoffuToken>();
            serviceCollection.AddScoped<IWoffuServices, WoffuServices>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }


    }
}

