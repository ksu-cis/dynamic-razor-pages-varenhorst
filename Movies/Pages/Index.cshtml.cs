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
        [BindProperty]
        public string SearchTerms { get; set; }

        /// <summary>
        /// Filtered MPAA ratings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        public string[] Genres { get; set; }

        /// <summary>
        /// the min IMDB
        /// </summary>
        public float IMDBMin { get; set; }

        /// <summary>
        /// the max IMDB
        /// </summary>
        public float IMDBMax { get; set; }



        public void OnGet(double? IMDBMin, double? IMDBMax)
        {
           
            //This does not work.
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings); 
            //Movies = MovieDatabase.FilterByGenre(Movies, Genres); 
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
        }

   


    }
}
