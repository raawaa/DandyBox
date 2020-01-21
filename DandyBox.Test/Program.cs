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
            //var movieInfo1 = Crawler.FetchMoiveInfo("LMPI-019");
            //var movieInfo2 = Crawler.FetchMoiveInfo("IPX-121");
            //using (var db = new DataContext())
            //{
            //    db.Database.Migrate();
            //    Movie movie1 = AddOrUpdateMovie(movieInfo1, db, "LMPI-019");
            //    Movie movie2 = AddOrUpdateMovie(movieInfo2, db, "IPX-121");
            //    db.SaveChanges();
            //}

            // Get mediafile on disk

            IEnumerable<MediaFile> mediaFiles = FileManager.GetMediaFiles(@"C:\Users\raawaa\Videos\DandyBoxTestMediaFiles").ToList();
            using (var _context = new DataContext())
            {
                _context.Database.Migrate();
                var mixedMediaFiles = new List<MediaFile>();
                foreach (var file in mediaFiles)
                {
                    Console.WriteLine($"process mediafile: ${file.FilePath}");
                    mixedMediaFiles.Add(_context.MediaFiles.SingleOrDefault(f => f.FilePath == file.FilePath) ?? file);
                }
                _context.MediaFiles.UpdateRange(mixedMediaFiles);
                _context.SaveChanges();
            }

            // Add new files to database. Check whether mediafile already in database(check filepath)
            // - no: add mediafile to database
            // - yes: add to database

            // Clean mediafiles in database which no longer exists (check filepath)

            // Parse Productcode, Add new movie entity

            Console.WriteLine("END OF INFO");
            Console.ReadLine();
        }

        /// <summary>
        /// Generate Movie entity from web crawled MovieInfo.
        /// </summary>
        /// <param name="fecthedMovie"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static Movie AddOrUpdateMovie(Movie fecthedMovie, DataContext db, string productId)
        {
            var movie = db.Movies
                .Include(m => m.MovieGenres)
                .Include(m => m.IdolMovies)
                .SingleOrDefault(m => m.ProductId == productId) ?? new Movie();

            movie.ProductId = fecthedMovie.ProductId;
            movie.Label = fecthedMovie.Label;
            movie.ReleaseDate = fecthedMovie.ReleaseDate;
            movie.Length = fecthedMovie.Length;
            movie.Studio = fecthedMovie.Studio;
            movie.Title = fecthedMovie.Title;
            movie.Series = fecthedMovie.Series;
            movie.MovieGenres = new List<MovieGenre>();
            movie.IdolMovies = new List<IdolMovie>();
            CheckExistingGenreAndIdol(fecthedMovie, db, movie);

            db.Movies.Update(movie);
            return movie;
        }

        private static void CheckExistingGenreAndIdol(Movie fecthedMovie, DataContext db, Movie movie)
        {
            fecthedMovie.MovieGenres.ForEach(
                mg =>
                {
                    var mmg = new MovieGenre
                    {
                        Genre = db.Genres.First(g => g.Name == mg.Genre.Name) ?? mg.Genre,
                        Movie = movie
                    };
                    movie.MovieGenres.Add(mmg);
                }
            );

            fecthedMovie.IdolMovies.ForEach(
                im =>
                {
                    var mim = new IdolMovie
                    {
                        Idol = db.Idols.First(i => i.Name == im.Idol.Name) ?? im.Idol,
                        Movie = movie
                    };
                    movie.IdolMovies.Add(mim);
                }
            );
        }
    }
}