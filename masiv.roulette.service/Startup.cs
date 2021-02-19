//-----------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API
{
    using System.Text.Json.Serialization;
    using Masiv.Roulette.API.Contracts;
    using Masiv.Roulette.API.Middleware.Cache;
    using Masiv.Roulette.API.Service;
    using Masiv.Roulette.API.Utilities;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

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
            var redisConnection = Configuration.GetConnectionString("Redis");
            services.AddTransient<IRouletteService, RouletteService>();
            services.AddTransient<IGenerateRandom, GenerateRandom>();
            services.AddTransient(typeof(ICacheMiddleware<>), typeof(CacheMiddleware<>));
            services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddSwaggerGen(c =>
            {   
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Masiv.Roulette.API", Version = "v1" });
            });
            services.AddDistributedRedisCache(options =>
                options.Configuration = redisConnection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Masiv.Roulette.API v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
