using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MuviMuviApi.Data.EntityFramework;

public static class DbContextUtilities
{
    public static ApplicationDbContext GetDbContext(WebApplicationFactory<Program> factory)
    {
        var scope = factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        return db;
    }
}