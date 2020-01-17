using DandyBox.Core;
using DandyBox.Core.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DandyBox.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var movieInfo1 = Crawler.GetMovieInfo("LMPI-019");
            var movieInfo2 = Crawler.GetMovieInfo("IPX-121");
            using (var db = new DataContext())
            {
                db.Database.Migrate();
                Movie movie1 = AddOrUpdateMovie(movieInfo1, db, "LMPI-019");
                Movie movie2 = AddOrUpdateMovie(movieInfo2, db, "IPX-121");
                db.SaveChanges();
            }
            Console.WriteLine("END OF INFO");
            Console.ReadLine();
        }

        /// <summary>
        /// Generate Movie entity from web crawled MovieInfo.
        /// </summary>
        /// <param name="movieInfo"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static Movie AddOrUpdateMovie(MovieInfo movieInfo, DataContext db, string productId)
        {
            var doUpdate = false;
            var movie = db.Movies
                .Include(m => m.MovieGenres)
                .Include(m => m.IdolMovies)
                .SingleOrDefault(m => m.ProductId == productId);
            if (movie != null)
            {
                doUpdate = true;
                movie.ProductId = movieInfo.ProductId;
                movie.Label = movieInfo.Label;
                movie.Director = movieInfo.Director;
                movie.ReleaseDate = movieInfo.ReleaseDate;
                movie.Length = movieInfo.Length;
                movie.Studio = movieInfo.Studio;
                movie.Title = movieInfo.Title;
                movie.Series = movieInfo.Series;
                movie.MovieGenres.Clear();
                movie.IdolMovies.Clear();
            }
            else
            {
                movie = new Movie
                {
                    ProductId = movieInfo.ProductId,
                    Label = movieInfo.Label,
                    ReleaseDate = movieInfo.ReleaseDate,
                    Length = movieInfo.Length,
                    Studio = movieInfo.Studio,
                    Title = movieInfo.Title,
                    Series = movieInfo.Series,
                    MovieGenres = new List<MovieGenre>(),
                    IdolMovies = new List<IdolMovie>()
                };
            }

            foreach (var genre in movieInfo.Genres)
            {
                Genre genreInDb = db.Genres
                    .SingleOrDefault(g => g.Name == genre.Name);
                if (genreInDb == null)
                {
                    movie.MovieGenres.Add(new MovieGenre { Movie = movie, Genre = genre });
                }
                else
                {
                    movie.MovieGenres.Add(new MovieGenre { Genre = genreInDb, Movie = movie });
                }
            }
            foreach (var idol in movieInfo.Idols)
            {
                Idol idolInDb = db.Idols.SingleOrDefault(i => i.Name == idol.Name);
                if (idolInDb == null)
                {
                    movie.IdolMovies.Add(new IdolMovie { Idol = idol, Movie = movie });
                }
                else
                {
                    movie.IdolMovies.Add(new IdolMovie { Idol = idolInDb, Movie = movie });
                }
            }
            if (doUpdate == false)
            {
                db.Movies.Add(movie);
            }
            else
            {
                db.Movies.Update(movie);
            }
            return movie;
        }
    }
}