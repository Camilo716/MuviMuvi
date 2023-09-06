using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using MuviMuviApi.Data.EntityFramework;
using MuviMuviApi.Models;
using Newtonsoft.Json;
using Test.Helpers;
using Tests.Helpers;

namespace Tests;

public partial class ControllerTests
{
    [Fact]
    public async Task GetGenreByIdReturnSuccessAndCorrectRecord()
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync($"api/genre/{_seedDataIds.GenresIds[0]}");

        var responseBody = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
        Assert.Contains("Action", responseBody);
    }

    [Fact]
    public async Task When_PostNewGenre_Then_GenresInDataBaseIncreased()
    {
        HttpClient client = _factory.CreateClient();   
        HttpContent genre = GenreUtilities.GetGenreHttpContent("Comedy");
        int counterBefore = await DbUtilities.GetGenreRecordCount(_context);

        HttpResponseMessage response = await client.PostAsync("api/genre", genre);

        int counterAfter = await DbUtilities.GetGenreRecordCount(_context);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(counterBefore+1, counterAfter);
        Assert.True(response.Headers.Contains("Location"), 
                "Headers don't contain location");
    }

    [Fact]
    public async Task Put_GenreReturnSuccess()
    {
        HttpClient client = _factory.CreateClient();   
        HttpContent newGenre = GenreUtilities.GetGenreHttpContent("NowIsAnotherGenre");

        HttpResponseMessage response = await client.PutAsync(
            $"api/genre/{_seedDataIds.GenresIds[0]}", newGenre);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updatedGenre = await GenreUtilities.GetGenreModelFromHttpResponse(response);
        Assert.Equal("NowIsAnotherGenre", updatedGenre.Name);
    } 

    [Fact]
    public async Task Delete_GenreReturnSuccess()
    {
        HttpClient client = _factory.CreateClient();   
        
        HttpResponseMessage response = await client.DeleteAsync(
            $"api/genre/{_seedDataIds.GenresIds[0]}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        HttpResponseMessage getResponse = await client.GetAsync(
            $"api/genre/{_seedDataIds.GenresIds[0]}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}