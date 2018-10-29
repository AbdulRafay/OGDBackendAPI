using MovieSearch.SearchDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieSearch
{
    public interface IMovies
    {
        DTOMovieSearchResults SearchMovie(string MovieName,int page);
    }
}
