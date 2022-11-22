using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class GameList
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(20, ErrorMessage = "Name length is too long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers are allowed.")]
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }

    }
}
