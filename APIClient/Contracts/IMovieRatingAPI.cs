using System;
using System.Collections.Generic;
using System.Text;
using APIClient.DTO;

namespace APIClient.Contracts
{
    public interface IMovieRatingAPI
    {
        MovieRatingsSearch GetMovieRatings(string id);
    }
}
