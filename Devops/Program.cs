using Devops.RabbitServices;
using Devops.RabbitServices.Interfaces;
using Devops.Services;
using Devops.Services.Interfaces;
using System.Net.Http.Headers;

namespace Devops
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient("DevAzureConnect", httpClient =>
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            });

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });

            
            builder.Services.Register();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static void Register(this IServiceCollection services)
        {
            services.AddTransient<IDevopsService, DevopsService>();
            services.AddScoped<IRabbitMqService, RabbitMqService>();
            services.AddScoped<IInfrastructureService, InfrastructureService>();
        }
    }
}