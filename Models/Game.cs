using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class Game
    {
        public int GameID { get; set; }

        public int GameListID { get; set; }

        public int RawgID { get; set; }

        public virtual GameList GameList { get; set; }
    }
}
