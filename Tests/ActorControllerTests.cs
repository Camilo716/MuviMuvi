using System.Text;
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

    [Fact]
    public async Task Get_AllActorsReturnSuccess()
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("api/actor");

        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
        Assert.True(true);
    }

    [Fact]
    public async Task GetByIdReturnSuccessAndCorrectRecord()
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync($"api/actor/{_seedActorsIds[0]}");

        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        Assert.Contains("Brad Pitt", responseBody);
    }
}