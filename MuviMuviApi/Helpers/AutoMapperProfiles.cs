using AutoMapper;
using MuviMuviApi.DTOs;
using MuviMuviApi.Models;

namespace MuviMuviApi.Helpers;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Genre, GenreDTO>(); 
        CreateMap<GenreCreationDTO, Genre>(); 
        
        CreateMap<Actor, ActorDTO>();
    }
}