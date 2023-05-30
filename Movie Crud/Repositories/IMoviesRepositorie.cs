using Car_Works.Models;
using Car_Works.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Car_Works.Repositories
{
    public interface IMoviesRepositorie
    {
        public List<Movie> GetAllMovies();

        public Movie GetMoviesById(int id);

        public void AddMovie(MovieViewModel movie);

        public void EditMovie(Movie movie,int id );

        public void DeleteMovie(int id);

        public MovieViewModel GetAllGenres();
    }
}
