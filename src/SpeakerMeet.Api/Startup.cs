using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpeakerMeet.Api.Config;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;
using SpeakerMeet.Core.Interfaces.Utilities;
using SpeakerMeet.Core.Services;
using SpeakerMeet.Infrastructure.Data.Repositories;
using SpeakerMeet.Infrastructure.Utilities;

namespace SpeakerMeet.Api
{
    [ExcludeFromCodeCoverage]
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
            services.AddCorsConfig();
            services.AddControllersConfig();
            services.AddDatabaseConfig(Configuration);
            services.AddSwaggerConfig();
            services.AddApplicationInsightsTelemetry();

            services.AddSingleton<ITimeManager, TimeManager>();
            services.AddScoped(typeof(ISpeakerMeetRepository), typeof(SpeakerMeetRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerConfig();
            app.UseCorsConfig();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}