using System.Text;
using Newtonsoft.Json;
using MuviMuviApi.DTOs;

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
}