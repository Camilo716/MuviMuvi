using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using MuviMuviApi.Data.EntityFramework;
using MuviMuviApi.Models;
using Newtonsoft.Json;
using Test.Helpers;
using Tests.Helpers;

namespace Tests;

public partial class ControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ApplicationDbContext _context;
    private readonly SeedDataIds _seedDataIds;

    public ControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _context = DbContextUtilities.GetDbContext(factory);
        _seedDataIds = DbUtilities.ReinitializeDbForTests(_context);
    }

    const string actorBaseUrl = "api/actor/";
    const string actorStringJson =  @"
    {
        ""Name"": ""Leonardo Di Caprio""
    }";

    [Theory]
    [InlineData("/api/genre")]
    [InlineData(actorBaseUrl)]
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
    [InlineData($"{actorBaseUrl}/-1")]
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

    [Theory]
    [InlineData(actorBaseUrl, actorStringJson)]
    public async Task Post_NewRecordReturnsSuccessAndRecordsInDbIncrease(string url, string jsonContent)
    {
        HttpClient client = _factory.CreateClient();  
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(url, httpContent);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.True(response.Headers.Contains("Location"), 
                "Headers don't contain location");
    }
}