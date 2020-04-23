﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {

        //the genres in the database.
        public static string[] genres;

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        static MovieDatabase() {

            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                HashSet<string> genreSet = new HashSet<string>();
                foreach (Movie movie in movies)
                {
                    if(movie.MajorGenre != null)
                    {
                        genreSet.Add(movie.MajorGenre);
                    }
                }
                genres = genreSet.ToArray();
            }
        }


    

        /// <summary>
        /// Gets the movie genres in the database.
        /// </summary>
        public static string[] Genres => genres;

        /// <summary>
        /// Searches the database for matching movies.
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        public static IEnumerable<Movie> Search(string terms)
        {
            List<Movie> results = new List<Movie>();
            if (terms == null) return All;

            foreach (Movie movie in All)
            {
                if (movie.Title != null && movie.Title.Contains(terms, StringComparison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }
            }

            return results;
        }


        private static List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        public static IEnumerable<Movie> All { get { return movies; } }

        public static string[] MPAARatings
        {
            get => new string[]
            {
                "G",
                "PG",
                "PG-13",
                "R",
                "NC-17"
            };
        }

        public static IEnumerable<Movie> FilterByMPAARating(IEnumerable<Movie> movies, IEnumerable<string> ratings)
        {
            // If no filter is specified, just return the provided collection 
            if (ratings == null || ratings.Count() == 0) return movies;
            List<Movie> results = new List<Movie>(); 
            foreach (Movie movie in movies) 
            { 
                if (movie.MPAARating != null && ratings.Contains(movie.MPAARating)) 
                {
                    results.Add(movie); 
                } 
            }
            return results;
        }

        public static IEnumerable<Movie> FilterByIMDBRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            var results = new List<Movie>();
            // If no filter is specified, just return the provided collection 
            if (min == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.IMDBRating <= max) results.Add(movie);
                }
                return results;
            }
            if(max == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.IMDBRating >= min) results.Add(movie);
                }
                return results;
            }

            foreach(Movie movie in movies)
            {
                if(movie.IMDBRating >= min && movie.IMDBRating <= max)
                {
                    results.Add(movie);
                }
            }
            return results;
        }

    }
}


