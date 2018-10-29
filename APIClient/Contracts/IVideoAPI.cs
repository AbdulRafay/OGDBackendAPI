using System;
using System.Collections.Generic;
using System.Text;
using APIClient.DTO;
namespace APIClient.Contracts
{
    public interface IVideoAPI
    {
        MovieSearchResults GetMovieDetails(string movieName,int page);
        VideosResults GetMovieVideos(int id);

    }
}
