using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MuviMuviApi.Data.EntityFramework;
using Test.Helpers;
using Tests.Helpers;

namespace Tests;

public class ActorControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ApplicationDbContext _context;
    private readonly List<int> _seedActorsIds;

    public ActorControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _context = GetDbContext();
        _seedActorsIds = DbUtilities.ReinitializeDbForTests(_context).ActorsIds!;
    }

    private ApplicationDbContext GetDbContext()
    {
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        return db;
    }
}