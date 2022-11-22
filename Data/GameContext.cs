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
                },
                new Game
                {
                    GameID = 2,
                    GameListID = 1,
                    RawgID = 494384
                },
                new Game
                {
                    GameID = 3,
                    GameListID = 1,
                    RawgID = 29179
                },
                new Game
                {
                    GameID = 4,
                    GameListID = 1,
                    RawgID = 39707
                },
                new Game
                {
                    GameID = 5,
                    GameListID = 1,
                    RawgID = 3265
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
                },
                new GameList
                {
                    ID = 3,
                    Name = "List 1"
                },
                new GameList
                {
                    ID = 4,
                    Name = "List 2"
                },
                new GameList
                {
                    ID = 5,
                    Name = "List 3"
                }
            );
        }
    }
}
