using System.ComponentModel.DataAnnotations;
using System.Net;
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

    [Theory]
    [InlineData("/api/genre/-1")]
    [InlineData("/api/actor/-1")]
    public async Task Get_When_IdIsNotvalid_Then_ReturnNotFound(string url)
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }  

    [Theory]
    [InlineData($"api/genre/-1")]
    public async Task Delete_When_RemoveRecord_And_ItDoesNotExist_Then_ReturnNotFound(string url)
    {
        HttpClient client = _factory.CreateClient();   
        
        HttpResponseMessage response = await client.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}