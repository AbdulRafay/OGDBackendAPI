using System;
using System.Collections.Generic;
using System.Text;
using APIClient.Contracts;
using APIClient.DTO;
using Microsoft.Extensions.Options;
using RestSharp;

namespace APIClient.Impl
{

    /*TODO:
     * move the api keys to the config!!
     */
    public class ImplVideoAPI : IVideoAPI
    {
        private IRestClient _restClient;
        private readonly IOptions<APIConfig> _config;
        public ImplVideoAPI(IRestClient RestClient, IOptions<APIConfig> config) {
            _restClient = RestClient;           
            _config = config;
            _restClient.BaseUrl = new Uri(_config.Value.video_api_base_url);
        }
        public MovieSearchResults GetMovieDetails(string movieName, int page)
        {            
            var request = new RestRequest("3/search/movie?api_key="+_config.Value.video_api_key+"&language=en-US&query="+ movieName + "&page="+page+"&include_adult=false", Method.GET);
            MovieSearchResults response2 = _restClient.Execute<MovieSearchResults>(request).Data;
            return response2;
        }

        public VideosResults GetMovieVideos(int id)
        {
            var request = new RestRequest("3/movie/"+id+ "?api_key=" + _config.Value.video_api_key + "&append_to_response=videos", Method.GET);
            VideosResults response2 = _restClient.Execute<VideosResults>(request).Data;
            return response2;
        }
    }
}
