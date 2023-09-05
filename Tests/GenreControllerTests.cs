using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MuviMuviApi.Data.EntityFramework;
using MuviMuviApi.Models;
using Newtonsoft.Json;
using Test.Helpers;
using Tests.Helpers;

namespace Tests;

public class GenreControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ApplicationDbContext _context;
    private readonly List<int> _seedGenresIds;

    public GenreControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _context = DbContextUtilities.GetDbContext(factory);
        _seedGenresIds = DbUtilities.ReinitializeDbForTests(_context).GenresIds!;
    }

    [Fact]
    public async Task Get_AllGenresReturnSuccess()
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("api/genre");

        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task GetByIdReturnSuccessAndCorrectRecord()
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync($"api/genre/{_seedGenresIds[0]}");

        var responseBody = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
        Assert.Contains("Action", responseBody);
    }

    [Fact]
    public async Task When_IdIsNotvalid_Then_ReturnNotFoundStatusCode()
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync($"/api/genre/{-1}");

        Assert.Equal("NotFound", response.StatusCode.ToString());
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
    public async Task Put_ReturnSuccess()
    {
        HttpClient client = _factory.CreateClient();   
        HttpContent newGenre = GenreUtilities.GetGenreHttpContent("NowIsAnotherGenre");

        HttpResponseMessage response = await client.PutAsync(
            $"api/genre/{_seedGenresIds[0]}", newGenre);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        string genreJson = await response.Content.ReadAsStringAsync();
        Genre genreUpdated = JsonConvert.DeserializeObject<Genre>(genreJson);
        Assert.Equal("NowIsAnotherGenre", genreUpdated.Name);
    } 

    [Fact]
    public async Task Delete_ReturnSuccess()
    {
        HttpClient client = _factory.CreateClient();   
        
        HttpResponseMessage response = await client.DeleteAsync(
            $"api/genre/{_seedGenresIds[0]}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        HttpResponseMessage getResponse = await client.GetAsync(
            $"api/genre/{_seedGenresIds[0]}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task When_DeleteGenre_And_GenreDoNotExist_Then_ReturnNotFound()
    {
        HttpClient client = _factory.CreateClient();   
        
        HttpResponseMessage response = await client.DeleteAsync(
            $"api/genre/-1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}