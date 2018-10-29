using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using APIClient.Contracts;
using APIClient.Impl;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MovieSearch.SearchDTOs;

namespace MovieSearch
{

    public class Movies : IMovies
    {
        private IVideoAPI _videoAPI;
        private IMovieRatingAPI _movieRatingAPI;
        private readonly IDistributedCache _distributedCache;
        private IMapper _mapper;
        private readonly IOptions<SearchConfig> _config;

        public Movies(IVideoAPI VideoAPI, IMovieRatingAPI MovieRatingAPI, IDistributedCache DistributedCache, IMapper Mapper, IOptions<SearchConfig> Config)
        {
            _videoAPI = VideoAPI;
            _movieRatingAPI = MovieRatingAPI;
            _distributedCache = DistributedCache;
            _mapper = Mapper;
            _config = Config;

        }

        public DTOMovieSearchResults SearchMovie(string MovieName,int page)
        {
            DTOMovieSearchResults SearchResult = null;

            if (_config.Value.cache)
            {
                var cacheKey = MovieName+":"+page;
                SearchResult = searchMovieInCache(cacheKey);
                if (SearchResult==null)//in case of exception
                {
                    SearchResult = _searchMovie(MovieName, page);
                    _cache(cacheKey,SearchResult);

                }
                
            }
            else {//this is the case when cache is turned off in config
                SearchResult = _searchMovie(MovieName,page);
            }

            return SearchResult;
        }

        /// <summary>
        /// Method to search movie in cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        private DTOMovieSearchResults searchMovieInCache(string cacheKey) {
            DTOMovieSearchResults rec = null;
            try
            {
                var existingRecord = _distributedCache.Get(cacheKey);
                if (existingRecord != null && existingRecord.Length > 0)
                {
                    using (var stream = new MemoryStream(existingRecord))
                    {
                        rec = new BinaryFormatter().Deserialize(stream) as DTOMovieSearchResults;
                    }
                }
            }
            catch (Exception ex)
            {

                //log exception here
            }
            return rec;
        }

        private DTOMovieSearchResults _searchMovie(string MovieName, int page) {
            DTOMovieSearchResults MappedVideoAPIResult = null;
            //get list of movies from movie video search api, containing movie ids and title            
            var VideoAPIResult = _videoAPI.GetMovieDetails(MovieName, page);
            MappedVideoAPIResult = _mapper.Map<DTOMovieSearchResults>(VideoAPIResult);



            //based on movie ids search for the related videos and external(imdbb) ids
            if (MappedVideoAPIResult != null)
            {
                foreach (var item in MappedVideoAPIResult.results)
                {
                    var VideoAPI_MovieTrailerResult = _videoAPI.GetMovieVideos(item.id);
                    if (!String.IsNullOrEmpty(VideoAPI_MovieTrailerResult.imdb_id))
                    {
                        item.imdb_id = VideoAPI_MovieTrailerResult.imdb_id;
                    }
                    foreach (var VideoItem in VideoAPI_MovieTrailerResult.videos.results)
                    {
                        if (!String.IsNullOrEmpty(VideoItem.key))
                        {
                            item.video_key = VideoItem.key;
                        }
                    }
                }
            }


            //search movie rating api(imdb) using the external id
            if (MappedVideoAPIResult != null)
            {
                foreach (var item in MappedVideoAPIResult.results)
                {
                    if (!String.IsNullOrEmpty(item.imdb_id))
                    {
                        var MovieRatingAPIResult = _movieRatingAPI.GetMovieRatings(item.imdb_id);
                        item.Rated = MovieRatingAPIResult.Rated;
                        item.Released = MovieRatingAPIResult.Released;
                        item.Year = MovieRatingAPIResult.Year;
                        item.Runtime = MovieRatingAPIResult.Runtime;
                        item.Genre = MovieRatingAPIResult.Genre;
                    }
                }
            }
            return MappedVideoAPIResult;
        }

        private void _cache(string CacheKey,DTOMovieSearchResults ObjToCache) {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, ObjToCache);
                var bytes = stream.ToArray();
                _distributedCache.Set(CacheKey, bytes);
            }
        }

    }

}
