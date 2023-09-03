using MuviMuviApi.Data.EntityFramework;
using MuviMuviApi.Models;

namespace Test.Helpers;


public static class Utilities
{
    public static void ReinitializeDbForTests(ApplicationDbContext db)
    {
        db.Genres.RemoveRange(db.Genres);
        InitializeDbForTests(db);
    }

    private static void InitializeDbForTests(ApplicationDbContext db)
    {
        db.Genres.AddRange(GetSeedingGenres());
        db.SaveChanges();
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