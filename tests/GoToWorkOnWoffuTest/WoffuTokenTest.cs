using System;
using Functions.Models;
using Functions.Services;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.Extensions;

namespace GoToWorkOnWoffuTest
{
    public class WoffuTokenTest : IClassFixture<Fixture.Fixture>
    {

        private readonly ServiceProvider _serviceProvider;

        public WoffuTokenTest(Fixture.Fixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void GetTokenTest()
        {
            var tokenContent = TestFiles.TestFiles.Token;
            var _mockToken = Substitute.For<IWoffuToken>();
            var token = _mockToken.Configure().GetToken(Arg.Any<string>(), Arg.Any<string>()).Returns(result =>
                {
                    return tokenContent;
                });

            Assert.Same(tokenContent, token);
        }

        [Fact]
        public void GetTokenToStringTest()
        {
            var context = _serviceProvider.GetService<IWoffuToken>();
            var token = TestFiles.TestFiles.Token;
            var result = context.GetTokenToString(JsonConvert.DeserializeObject<JwtModel>(token));

            Assert.True(result.UserId != 0);
            Assert.True(result.UserId == 62014);
        }
    }
}
