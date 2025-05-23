using GamesApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.Data;

public class GamesDbContext: DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public GamesDbContext(DbContextOptions<GamesDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>().HasQueryFilter(game => !game.IsDeleted);
        modelBuilder.Entity<Genre>().HasQueryFilter(genre => !genre.IsDeleted);
    }
}