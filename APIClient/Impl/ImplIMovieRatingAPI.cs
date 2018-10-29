using System;
using System.Collections.Generic;
using System.Text;
using APIClient.Contracts;
using APIClient.DTO;
using Microsoft.Extensions.Options;
using RestSharp;

namespace APIClient.Impl
{

    /// <summary>
    /// Implements the IMovieRatingAPI interface to get movie info from a movie database, in this case its imdb
    /// returns MovieRatings DTO
    /// </summary>
    public class ImplIMovieRatingAPI : IMovieRatingAPI
    {
        private IRestClient _restClient;
        private readonly IOptions<APIConfig> _config;
        public ImplIMovieRatingAPI(IRestClient RestClient, IOptions<APIConfig> config)
        {
            _restClient = RestClient;
            _config = config;
            _restClient.BaseUrl = new Uri(_config.Value.rating_api_base_url);
        }
        public MovieRatingsSearch GetMovieRatings(string id)
        {            
            var request = new RestRequest("?i=" + id + "&apikey=" + _config.Value.rating_api_key, Method.GET);
            MovieRatingsSearch response2 = _restClient.Execute<MovieRatingsSearch>(request).Data;
            return response2;
        }
    }
}
