namespace FilmDatabaseSysteem.Models
{
    public class Film
    {
        public List<int> MovieIds { get; set; }

        public Film(List<int> movieIds)
        {
            MovieIds = movieIds;
        }
    }
}
