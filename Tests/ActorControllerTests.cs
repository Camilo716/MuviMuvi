using Microsoft.AspNetCore.Mvc.Testing;
using MuviMuviApi.Data.EntityFramework;
using Test.Helpers;

namespace Tests;

public class ActorControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ApplicationDbContext _context;
    private readonly List<int> _seedActorsIds;

    public ActorControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _context = DbContextUtilities.GetDbContext(factory);
        _seedActorsIds = DbUtilities.ReinitializeDbForTests(_context).ActorsIds!;
    }
}