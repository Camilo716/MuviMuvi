using Microsoft.EntityFrameworkCore;
using MuviMuviApi.Data.EntityFramework;
using MuviMuviApi.Models;

namespace Test.Helpers;

public static class DbUtilities
{
    public static Task<int> GetGenreRecordCount(ApplicationDbContext db)
    {
        return db.Genres.CountAsync();
    }

    public static SeedDataIds ReinitializeDbForTests(ApplicationDbContext db)
    {
        db.Genres.RemoveRange(db.Genres);
        SeedDataIds seedData =  InitializeDbForTests(db);
        return seedData;
    }

    private static SeedDataIds InitializeDbForTests(ApplicationDbContext db)
    {
        var seedGenres = GetSeedingGenres();
        db.Genres.AddRange(seedGenres);
        db.SaveChanges();

        List<int> seedGenresIds = seedGenres.Select(genre => genre.Id).ToList();

        SeedDataIds seedData = new SeedDataIds(seedGenresIds);
        return seedData;
    }

    private static List<Genre> GetSeedingGenres()
    {
        return new List<Genre>()
        {
            new Genre(){Name="Action" },
            new Genre(){Name="Horror" },
            new Genre(){Name="Fantasy"}
        };
    }
}

public class SeedDataIds
{
    public List<int>? GenresIds { get; set;}
    public List<int>? ActorsIds { get; set;}

    public SeedDataIds(List<int> GenresIds, List<int> ActorsIds)
    {
        this.GenresIds = GenresIds;
        this.ActorsIds = ActorsIds;   
    }
    public SeedDataIds(List<int> GenresIds)
    {
        this.GenresIds = GenresIds; 
    }
}