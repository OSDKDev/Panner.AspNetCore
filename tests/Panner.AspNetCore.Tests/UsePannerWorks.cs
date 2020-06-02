namespace Panner.AspNetCore.Tests
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Panner.Builders;
    using System;
    using System.Linq;
    using Xunit;

    public class UsePannerWorks
    {
        public IServiceProvider GetServiceProvider(Action<PContextBuilder> configuration)
            => new ServiceCollection()
                .UsePanner(configuration)
                .BuildServiceProvider();

        [Fact]
        public void AddsContextToServiceProvider()
        {
            var x = GetServiceProvider(x => { });
            Assert.NotNull(x);
        }

        [Fact]
        public void AddsBinderProvider()
        {
            var mvcOptions = GetServiceProvider(x => { })
                .GetService<IOptions<MvcOptions>>();

            Assert.NotNull(mvcOptions);

            var foundProvider = mvcOptions.Value.ModelBinderProviders
                .Any(x => x.GetType() == typeof(ParticlesBinderProvider));

            Assert.True(foundProvider);
        }

        [Fact]
        public void ConfigurationExecutes()
        {
            var executed = false;

            var mvcOptions = GetServiceProvider(x => {
                executed = true;
            });

            Assert.True(executed);
        }
    }
}
