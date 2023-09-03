using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Sdk;

namespace Tests;

public class Get_EnpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public Get_EnpointsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory; 
    }

    [Theory]
    [InlineData("/api/genre")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
    }
}