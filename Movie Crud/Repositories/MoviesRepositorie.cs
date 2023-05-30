using Azure.Core;
using Car_Works.Models;
using Car_Works.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Car_Works.Repositories
{
    public class MoviesRepositorie : IMoviesRepositorie
    {
        AplicationDbContext Context;

        public MoviesRepositorie(AplicationDbContext _context)
        {
            Context = _context;
        }
        public void AddMovie(MovieViewModel newmodel)
        {

            Movie objmovie = new Movie();
            if (newmodel!=null)
            {
                objmovie.Title = newmodel.Title;
                objmovie.Rate = newmodel.Rate;
                objmovie.Year = newmodel.Year;
                objmovie.GenreId=newmodel.GenreId;
                objmovie.StoreLine=newmodel.Storeline;
                objmovie.Poster = newmodel.Poster!;
                Context.Movies.Add(objmovie);
                Context.SaveChanges();
            }
           
        }

        public void DeleteMovie(int id)
        {
            var movie = GetMoviesById(id);
            Context.Remove(movie);
            Context.SaveChanges();
        }

        public void EditMovie(Movie _movie, int Id)
        {
             Movie oldmovie = GetMoviesById(Id);
            oldmovie.Title = _movie.Title;
            oldmovie.Year = _movie.Year;
            oldmovie.Rate = _movie.Rate;
            oldmovie.Poster = _movie.Poster;
            oldmovie.StoreLine= _movie.StoreLine;
            Context.SaveChanges();
        }

        public MovieViewModel GetAllGenres()
        {
            var viewGeneres = new MovieViewModel();
            viewGeneres.Genres = Context.Genres.ToList();
            return viewGeneres; 
        }

        public List<Movie> GetAllMovies()
        {
            List<Movie> movies = Context.Movies.ToList();
            return movies;
        }

        public Movie GetMoviesById(int id)
        {
           var movie =Context.Movies.FirstOrDefault(m=>m.Id==id);
            return movie;
        }


    }
}
