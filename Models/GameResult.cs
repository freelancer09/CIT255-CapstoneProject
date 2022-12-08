using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace CapstoneProject.Models
{
    public class GameResult
    {
        public int Id { get; set; }

        public string Slug { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Metacritic { get; set; }

        public string Released { get; set; }

        public List<PlatformWrapper> Platforms { get; set; }

        public Developer[] Developers { get; set; }

        public Genre[] Genres { get; set; }

        public Publisher[] Publishers { get; set; }

        public ESRBRating esrb_rating { get; set; }
    }
}
