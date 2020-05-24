using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using IngaiaInterview.Application.Infrastructure;
using IngaiaInterview.Application.Playlist.Queries.GetPlaylistForCity;
using IngaiaInterview.Domain.Music;
using IngaiaInterview.Domain.Weather;
using IngaiaInterview.Infrastructure;
using IngaiaInterview.Repository;
using IngaiaInterview.Repository.Music;
using IngaiaInterview.Repository.Weather;
using IngaiaInterview.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NodaTime;

namespace IngaiaInterview.WebApi
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

            services.AddHttpClient();

            services.AddHttpClient<IWeatherDataRepository, WeatherDataRepository>();

            services.AddTransient<IPlaylistRepository, PlaylistRepository>();

            services.AddMediatR(typeof(GetPlaylistForCityQuery).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddSingleton<IClock>(SystemClock.Instance);

            services.AddTransient<EnvironmentSettings>(s =>
            {
                EnvironmentSettings settings = new EnvironmentSettings();

                var builder = new ConfigurationBuilder()
                                    .AddJsonFile("appSettings.json", optional: true)
                                    .AddEnvironmentVariables();
                var config = builder.Build();

                ConfigurationBinder.Bind(config, settings);

                services.Configure<EnvironmentSettings>(options => Configuration.GetSection("EnviromentSettings").Bind(options));

                return settings;
            });

            services.AddDbContext<IngaiaInterviewDbContext>(options =>
            options.UseNpgsql(ConvertUrlConnectionString(Configuration.GetValue<string>("DATABASE_URL"))));

            services.AddMvc(options => options.Filters.Add(typeof(ExceptionsFilterAttribute)))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetPlaylistForCityQueryValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ingaia Interview", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ingaia Interview");
                c.RoutePrefix = "";
            });
        }

        private static string ConvertUrlConnectionString(string url)
        {
            var uri = new Uri(url);
            var host = uri.Host;
            var port = uri.Port;
            var database = uri.Segments.Last();
            var parts = uri.AbsoluteUri.Split(':', '/', '@');
            var user = parts[3];
            var password = parts[4];

            return $"host={host}; port={port}; database={database}; username={user}; password={password}; SSL Mode=Prefer; Trust Server Certificate=true";
        }
    }
}
