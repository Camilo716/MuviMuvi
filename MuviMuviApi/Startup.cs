using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MuviMuviApi.Data.EntityFramework;
using MuviMuviApi.Data.Repositories;
using MuviMuviApi.Models;
using MuviMuviApi.Services;

namespace MuviMuviApi;

public class Startup
{
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
        _config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup));

        services.AddControllers()
                .AddJsonOptions(
                    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        
        services.AddDbContext<ApplicationDbContext>(
            options => 
                // options.UseSqlServer(_config.GetConnectionString("defaultConnection"))
                options.UseInMemoryDatabase("InMemory")
            );
  
        services.AddScoped<GenreService>();
        services.AddScoped<ActorService>();

        services.AddScoped<IRepository<Genre>, EfRepository<Genre>>();
        services.AddScoped<IRepository<Actor>, EfRepository<Actor>>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void ConfigureMiddlewares(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
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