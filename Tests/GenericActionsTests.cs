using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Testing;
using MuviMuviApi.Data.EntityFramework;
using Test.Helpers;

namespace Tests;

public class GenericActionsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ApplicationDbContext _context;
    private readonly List<int> _seedActorsIds;

    public GenericActionsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _context = DbContextUtilities.GetDbContext(factory);
        _seedActorsIds = DbUtilities.ReinitializeDbForTests(_context).ActorsIds!;
    }

    [Theory]
    [InlineData("/api/genre")]
    [InlineData("/api/actor")]
    public async Task Get_AllRecordsReturnSuccess(string url)
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
        Assert.True(true);
    }
}