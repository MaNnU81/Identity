using identity.service.model;
using Identity.Api.Interfaces;
using Identity.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

namespace Identity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddControllers();

            builder.Services.AddScoped<IUserService, UserService>();
            

         


            builder.Services.AddControllers();

         

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "Identity API",
                    Version = "v1",
                    Description = "API for Identity, a social media platform for sharing chirps."
                });
            });

            var app = builder.Build();


            app.UseSwagger(c =>
            {

                c.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v3/swagger.json", "Identity API V1");
               

            });
            builder.Services.AddLogging();


            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
