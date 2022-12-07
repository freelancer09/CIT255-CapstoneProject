using System.Collections;
using System.Security.Policy;
using System.Collections.Generic;

namespace CapstoneProject.Models
{
    public class GameListViewModel<T>
    {
        public GameList gameList { get; set; }
        public List<T> gameLists { get; set; }

        public List<GameResult> gameResults { get; set; }
    }
}
