using api.Domain;
using api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api;

internal class Program
{
    static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddCors((CorsOptions corsOptions) => {
            corsOptions.AddDefaultPolicy((CorsPolicyBuilder corsPolicyBuilder) => {
                corsPolicyBuilder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        // Singleton: instance per 'deploy' (per application lifetime)
        // Scoped: instance per HTTP request
        // Transient: instance per code request.
        builder.Services.AddScoped<DbContext, MongoDbContext>(); // Apperently its best practise to have it a singleton. I dont see a reason to leave a db connection open for ever. So I stick to scoped. Retrieving a connection from to pool and returning it once per http request seems more reasonable.

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else // prevent warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3] Failed to determine the https port for redirect.
            app.UseHttpsRedirection();

        app.UseCors();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
