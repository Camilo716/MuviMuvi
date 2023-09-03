using AutoMapper;
using MuviMuviApi.Models;

namespace MuviMuviApi.Helpers;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Genre, GenreDto>().ReverseMap(); 
    }
}