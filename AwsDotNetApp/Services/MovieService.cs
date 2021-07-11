using MovieRank.Contracts;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AwsDotNetApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRankRepository movieRankRepository;
        private readonly IMapper mapper;
        public MovieService(IMovieRankRepository moveRankRepository, IMapper mapper)
        {
            this.movieRankRepository = moveRankRepository;
            this.mapper = mapper;
        }

        public async Task AddMovie(int userId, MovieRankRequest movieRankRequest)
        {
            var movieDb = mapper.ToMovieDbModel(userId, movieRankRequest);
            await this.movieRankRepository.AddMovie(movieDb);
        }

        public async Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase()
        {
            var response = await movieRankRepository.GetAllItems();

            return this.mapper.ToMovieContract(response);
        }

        public async Task<MovieResponse> GetMovie(int userId, string movieName)
        {
            var response = await movieRankRepository.GetMovie(userId, movieName);

            return this.mapper.ToMovieContract(response);
        }

        public async Task<MovieRankResponse> GetMovieRank(string movieName)
        {
            var response = await this.movieRankRepository.GetMovieRank(movieName);
            var overallMovieRanking = Math.Round(response.Select(x =>x.Ranking).Average());
            return new MovieRankResponse
            {
                MovieName = movieName,
                OverallRanking = overallMovieRanking
            };
        }

        public async Task<IEnumerable<MovieResponse>> GetUserRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var response = await this.movieRankRepository.GetUserRankedMoviesByMovieTitle(userId, movieName);

            return this.mapper.ToMovieContract(response);
        }

        public async Task UpdateMovie(int userId, MovieUpdateRequest request)
        {
            var response = await this.movieRankRepository.GetMovie(userId, request.MovieName);
            var movieDb = this.mapper.ToMovieDbModel(userId, response, request);
            await this.movieRankRepository.UpdateMovie(movieDb);
        }
    }
}