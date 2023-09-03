using System.Text;
using MuviMuviApi.Models;
using Newtonsoft.Json;

namespace Tests.Helpers;

internal static class GenreUtilities
{
    internal static HttpContent GetGenreHttpContent(string name)
    {
        var genre = new Genre { Name = name };
        var jsonContent = JsonConvert.SerializeObject(genre);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return httpContent;
    }
}