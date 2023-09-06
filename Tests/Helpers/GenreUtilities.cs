using System.Text;
using MuviMuviApi.Models;
using Newtonsoft.Json;

namespace Tests.Helpers;

internal static class GenreUtilities
{
    internal static HttpContent GetGenreHttpContent(string name)
    {
        var genre = new GenreCreationDTO { Name = name };
        var jsonContent = JsonConvert.SerializeObject(genre);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return httpContent;
    }

    internal static async Task<Genre> GetGenreModelFromHttpResponse(HttpResponseMessage response)
    {
        string genreJson = await response.Content.ReadAsStringAsync();
        Genre genreModel = JsonConvert.DeserializeObject<Genre>(genreJson);
        return genreModel;
    }
}