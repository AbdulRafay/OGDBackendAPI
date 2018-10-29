using System;
using System.Collections.Generic;
using System.Text;

namespace APIClient.DTO
{
    /// <summary>
    /// DTOs for movie rating search aka imdb, could be any in a homogenous compliant world of APIs
    /// </summary>

    public class MovieRatingsSearch
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
    }
}
