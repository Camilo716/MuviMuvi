using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MuviMuviApi.Data.EntityFramework;
using Test.Helpers;
using Xunit.Sdk;

namespace Tests;

public class GenreControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public GenreControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;

        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        Utilities.ReinitializeDbForTests(db);
    }

    [Theory]
    [InlineData("/api/genre")]
    public async Task Get_GeneralEndpointsReturnSuccessAndCorrectContentType(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task When_IdIsNotvalid_Then_ReturnNotFoundStatusCode()
    {
        var httpClient = _factory.CreateClient();

        var response = await httpClient.GetAsync($"/api/genre/{-1}");

        Assert.Equal("NotFound", response.StatusCode.ToString());
    }    
}