using System;
using System.Collections.Generic;
using System.Text;

namespace APIClient.DTO
{
    /*TODO:
     * set the naming convention, upper case props values its not JS!
    */


        /// <summary>
        /// DTOs for video search and description
        /// </summary>
   
    public class Movie
    {
        public int id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }       
    }

    public class MovieSearchResults
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Movie> results { get; set; }
    }



    public class Video
    {
        public string key { get; set; }      
        public string site { get; set; }
        public int size { get; set; }
        public string type { get; set; }
    }

    public class Videos
    {
        public List<Video> results { get; set; }
    }

    public class VideosResults
    {
        public string imdb_id { get; set; }
        public Videos videos { get; set; }
    }
}
