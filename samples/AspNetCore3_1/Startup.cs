using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Panner.AspNetCore.Samples.AspNetCore3_1.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Panner.Builders;
using Panner.AspNetCore.Samples.AspNetCore3_1.PannerExtensions;

namespace Panner.AspNetCore.Samples.AspNetCore3_1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.UsePanner(c =>
            {
                c.Entity<Post>()
                    .IsSortableByPopularity()
                    .Property(x => x.Id, o => o
                        .IsSortableAs(nameof(Views.Post.Id))
                        .IsFilterableAs(nameof(Views.Post.Id))
                    )
                    .Property(x => x.Title, o => o
                        .IsSortableAs(nameof(Views.Post.Title))
                    )
                    .Property(x => x.CreatedOn, o => o
                        .IsSortableAs(nameof(Views.Post.Creation))
                        .IsFilterableAs(nameof(Views.Post.Creation))
                    );
            });

            services.AddDbContext<BlogContext>(options =>
            {
                options.UseInMemoryDatabase("BlogDb");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
