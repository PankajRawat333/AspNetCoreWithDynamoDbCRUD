using Amazon;
using Amazon.DynamoDBv2;
using AwsDotNetApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;

namespace AwsDotNetApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddDefaultAWSOptions(
                new Amazon.Extensions.NETCore.Setup.AWSOptions
                {
                    Region = RegionEndpoint.GetBySystemName("ap-south-1")
                });
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IMovieRankRepository, MovieRankRepository>(); ;
            services.AddSingleton<IMapper, Mapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}