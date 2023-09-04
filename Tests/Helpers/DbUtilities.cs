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

    public static List<int> ReinitializeDbForTests(ApplicationDbContext db)
    {
        db.Genres.RemoveRange(db.Genres);
        var seedGenresIds =  InitializeDbForTests(db);
        return seedGenresIds;
    }

    private static List<int> InitializeDbForTests(ApplicationDbContext db)
    {
        var seedGenres = GetSeedingGenres();
        db.Genres.AddRange(seedGenres);
        db.SaveChanges();

        var seedGenresIds = seedGenres.Select(genre => genre.Id).ToList();
        return seedGenresIds;
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