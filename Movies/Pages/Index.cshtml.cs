using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The moveis to display on the index page.
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// the current search terms.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; }

        /// <summary>
        /// Filtered MPAA ratings
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] Genres { get; set; }

        /// <summary>
        /// the min IMDB
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// the max IMDB
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }



        public void OnGet()
        {
            Movies = MovieDatabase.All;

            //Search movie title for the search terms.
            if(SearchTerms != null)
            {
                Movies = Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase));
            }

            //filter through MPAARatings
            if(MPAARatings != null && MPAARatings.Length!=0)
            {
                Movies = Movies.Where(movie =>
                    movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating)
                );
            }

            //Filter through genres
            if (Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie =>
                    movie.MajorGenre != null && Genres.Contains(movie.MajorGenre)
                );
            }

            // Filter through IMDB Ratings
            if (IMDBMin == null && IMDBMax != null)
            {
                Movies = Movies.Where(movie =>
                    movie.IMDBRating != null && IMDBMax >= movie.IMDBRating 
                );
            }

            if (IMDBMin != null && IMDBMax == null)
            {
                Movies = Movies.Where(movie =>
                    movie.IMDBRating != null && IMDBMin <= movie.IMDBRating
                );
            }

            if(IMDBMin != null && IMDBMax != null)
            {
                Movies = Movies.Where(movie =>

                   movie.IMDBRating != null && IMDBMin <= movie.IMDBRating && IMDBMax >= movie.IMDBRating
                );
            }
        }

   


    }
}
