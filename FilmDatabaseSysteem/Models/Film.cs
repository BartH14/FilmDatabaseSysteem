namespace FilmDatabaseSysteem.Models
{
    public class Film
    {
        public List<int> MovieIds { get; }

        public Film(List<int> movieIds)
        {
            MovieIds = movieIds;
        }
    }
}