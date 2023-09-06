using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using MuviMuviApi.Data.EntityFramework;
using Test.Helpers;
using Tests.Helpers;

namespace Tests;

public partial class ControllerTests
{
    [Fact]
    public async Task GetByIdReturnSuccessAndCorrectRecord()
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync($"api/actor/{_seedDataIds.ActorsIds[0]}");

        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        Assert.Contains("Brad Pitt", responseBody);
    }

    [Fact]
    public async Task When_PostNewActor_Then_ActorsInDataBaseIncreased()
    {
        HttpClient client = _factory.CreateClient();   
        HttpContent actor = ActorUtilities.GetActorHttpContent("Leonardo Di Caprio");
        int counterBefore =  await DbUtilities.GetActorRecordCount(_context);

        HttpResponseMessage response = await client.PostAsync("api/actor", actor);

        int counterAfter = await DbUtilities.GetActorRecordCount(_context);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(counterBefore+1, counterAfter);
        Assert.True(response.Headers.Contains("Location"), 
                "Headers don't contain location");
    }

    [Fact]
    public async Task Put_ActorReturnSuccess()
    {
        HttpClient client = _factory.CreateClient();   
        HttpContent newActor = ActorUtilities.GetActorHttpContent("Actor Updated");

        HttpResponseMessage response = await client.PutAsync(
            $"api/actor/{_seedDataIds.ActorsIds[0]}", newActor);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updatedActor = await ActorUtilities.GetActorModelFromHttpResponse(response);
        Assert.Equal("Actor Updated", updatedActor.Name);
    } 
}