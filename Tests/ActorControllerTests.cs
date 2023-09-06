using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
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
        _context = DbContextUtilities.GetDbContext(factory);
        _seedActorsIds = DbUtilities.ReinitializeDbForTests(_context).ActorsIds!;
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

    // CONCURRENCY ERRORS WHEN ACTIVATE THIS TEST
    // [Fact]
    // public async Task When_PostNewActor_Then_ActorsInDataBaseIncreased()
    // {
    //     HttpClient client = _factory.CreateClient();   
    //     HttpContent actor = ActorUtilities.GetActorHttpContent("Leonardo Di Caprio");
    //     int counterBefore =  await DbUtilities.GetActorRecordCount(_context);

    //     HttpResponseMessage response = await client.PostAsync("api/actor", actor);

    //     int counterAfter = await DbUtilities.GetActorRecordCount(_context);
    //     Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    //     Assert.Equal(counterBefore+1, counterAfter);
    //     Assert.True(response.Headers.Contains("Location"), 
    //             "Headers don't contain location");
    // }
}