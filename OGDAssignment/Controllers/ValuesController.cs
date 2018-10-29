using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieSearch;
using MovieSearch.SearchDTOs;

namespace OGDAssignment.Controllers
{
    /*TODO:
     * Implement Error and Exception logging, Elmah or something
     */
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IMovies _searchAPI;
        public ValuesController(IMovies SearchAPI) {
            _searchAPI = SearchAPI;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            ////Movies x = new Movies();
            //var search = _searchAPI.SearchMovie("pulp fiction");
            return "What you seek is seeking you";
        }

        // GET api/values/5/3
        [EnableCors("MyPolicy")]       
        [HttpGet("{MovieName}/{pageNumber}")]
        public ActionResult<DTOMovieSearchResults> Get(string MovieName,int pageNumber)
        {
            //Movies x = new Movies();
            var search = _searchAPI.SearchMovie(MovieName, pageNumber);
            return search;
        }
       
    }
}
