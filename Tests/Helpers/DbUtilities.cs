using Microsoft.EntityFrameworkCore;
using MuviMuviApi.Data.EntityFramework;
using MuviMuviApi.Models;

namespace Test.Helpers;

public static class DbUtilities
{
    public static async Task<int> GetGenreRecordCount(ApplicationDbContext db)
    {
        return await db.Genres.CountAsync();
    }

    public static async Task<int> GetActorRecordCount(ApplicationDbContext db)
    {
        var actors = db.Actors;
        int counter = await actors.CountAsync();
        return counter;
    }

    public static SeedDataIds ReinitializeDbForTests(ApplicationDbContext db)
    {
        // throw new Exception(db.ContextId.ToString());
        db.Genres.RemoveRange(db.Genres);
        db.Actors.RemoveRange(db.Actors);
        SeedDataIds seedData =  InitializeDbForTests(db);
        return seedData;
    }

    private static SeedDataIds InitializeDbForTests(ApplicationDbContext db)
    {
        var seedGenres = GetSeedingGenres();
        var seedActors = GetSeedingActors();        

        db.Genres.AddRange(seedGenres);
        db.Actors.AddRange(seedActors);
        db.SaveChanges();

        List<int> seedGenresIds = seedGenres.Select(genre => genre.Id).ToList();
        List<int> seedActorsIds = seedActors.Select(actor => actor.Id).ToList();

        SeedDataIds seedData = new SeedDataIds(seedGenresIds, seedActorsIds);
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

    private static List<Actor> GetSeedingActors()
    {
        return new List<Actor>
        {
            new Actor(){Name="Brad Pitt"},
            new Actor(){Name="Tom Cruise"},
            new Actor(){Name="Ben Affleck"}
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