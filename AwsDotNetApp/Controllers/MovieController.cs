using AwsDotNetApp.Services;
using Microsoft.AspNetCore.Mvc;
using MovieRank.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsDotNetApp.Controllers
{
    [Route("movies")]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        [HttpGet]
        public async Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase()
        {
            var result = await this.movieService.GetAllItemsFromDatabase();
            return result;
        }

        [HttpGet]
        [Route("{userId}/{movieName}")]
        public async Task<MovieResponse> GetMovie(int userId, string movieName)
        {
            var result = await this.movieService.GetMovie(userId, movieName);
            return result;
        }

        [HttpGet]
        [Route("user/{userId}/rankedMovies/{movieName}")]
        public async Task<IEnumerable<MovieResponse>> GetUserRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var result = await this.movieService.GetUserRankedMoviesByMovieTitle(userId, movieName);
            return result;
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> AddMovie(int userId, [FromBody] MovieRankRequest movieRankRequest)
        {
            await this.movieService.AddMovie(userId, movieRankRequest);
            return Ok();
        }

        [HttpPatch]
        [Route("{userId}")]
        public async Task<IActionResult> UpdateMovie(int userId, [FromBody] MovieUpdateRequest movieRankRequest)
        {
            await this.movieService.UpdateMovie(userId, movieRankRequest);
            return Ok();
        }

        [HttpGet]
        [Route("{movieName}/ranking")]
        public async Task<MovieRankResponse> GetMovieRanking(string movieName)
        {
            var result = await this.movieService.GetMovieRank(movieName);
            return result;
        }
    }
}
