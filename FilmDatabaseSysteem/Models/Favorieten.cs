using FilmDatabaseSysteem.Pages;

namespace FilmDatabaseSysteem.Models
{
    public class Favorieten
    {
        public int UserId { get; set; }
        public List<int> MovieIds { get; set; }

        public Favorieten(int userId, List<int> movieIds)
        {
            UserId = userId;
            MovieIds = movieIds;
        }
    }
}