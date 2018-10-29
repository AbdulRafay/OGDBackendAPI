using System;
using System.Collections.Generic;
using System.Text;

namespace MovieSearch.SearchDTOs
{
    [Serializable]
    public class DTOMovie
    {
        public int id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        //public DTOVideosResults videosearchresults { get; set; }
        //public DTOMovieRatingsSearch movieRatings { get; set; }
        //experiment
        public string imdb_id { get; set; }
        public string video_key { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
    }

    [Serializable]
    public class DTOMovieSearchResults
    {
        public DTOMovieSearchResults() {
            results = new List<DTOMovie>();
        }
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<DTOMovie> results { get; set; }
    }





    public class DTOVideo
    {
        public string key { get; set; }
        public string site { get; set; }
        public int size { get; set; }
        public string type { get; set; }
    }

    public class DTOVideos
    {
        public List<DTOVideo> results { get; set; }
    }

    public class DTOVideosResults
    {
        public string imdb_id { get; set; }
        public DTOVideos videos { get; set; }
    }



    public class DTOMovieRatingsSearch
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
    }
}
