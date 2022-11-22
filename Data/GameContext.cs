using CapstoneProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameList> GameLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Game>().HasData(
                new Game {
                    GameID = 1,
                    GameListID = 1,
                    RawgID = 58175
                }
            );

            modelBuilder.Entity<GameList>().HasData(
                new GameList {
                    ID = 1,
                    Name = "Collection"
                },
                new GameList
                {
                    ID = 2,
                    Name = "Wish List"
                }
            );
        }
    }
}
