using Functions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GoToWorkOnWoffuTest.Fixture
{

    public class Fixture
    {
        public Fixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IWoffuToken, WoffuToken>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}

