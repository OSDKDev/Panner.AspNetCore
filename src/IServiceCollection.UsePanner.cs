namespace Panner.AspNetCore
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Panner.Builders;
    using System;

    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UsePanner(this IServiceCollection services, Action<PContextBuilder> configure)
        {
            var contextBuilder = new PContextBuilder();
            configure(contextBuilder);
            services.AddSingleton<IPContext>(contextBuilder.Build());

            services.Configure<MvcOptions>(options =>
            {
                options.ModelBinderProviders.Insert(0, new ParticlesBinderProvider());
            });

            return services;
        }
    }
}
