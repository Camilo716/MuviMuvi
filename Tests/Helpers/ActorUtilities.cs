using System.Text;
using Newtonsoft.Json;
using MuviMuviApi.DTOs;
using MuviMuviApi.Models;

namespace Tests.Helpers;

internal static class ActorUtilities
{
    internal static HttpContent GetActorHttpContent(string name)
    {
        var actorCreationDTO = new ActorCreationDTO { Name = name };
        var jsonContent = JsonConvert.SerializeObject(actorCreationDTO);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return httpContent;
    }

    internal static async Task<Actor> GetActorModelFromHttpResponse(HttpResponseMessage response)
    {
        string actorJson = await response.Content.ReadAsStringAsync();
        Actor actorModel = JsonConvert.DeserializeObject<Actor>(actorJson);
        return actorModel;
    }
}