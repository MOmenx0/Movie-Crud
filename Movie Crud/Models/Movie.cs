using System.ComponentModel.DataAnnotations;

namespace Car_Works.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required,MaxLength(250)]
       public string Title { get; set; }

        public int Year { set; get; }
        [Required]
        public double Rate { set; get; }
        public string StoreLine { get; set; }
        [Required]
        public byte[] Poster { set; get;  }

        public byte GenreId { set; get; }

        public Genre Genre { set; get; }
    }
}
