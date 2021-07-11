using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Libs.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<MovieResponse> ToMovieContract(IEnumerable<MovieDb> items)
        {
            return items.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(MovieDb movie)
        {
            return new MovieResponse
            {
                MovieName = movie.MovieName,
                Actors = movie.Actors,
                Description = movie.Description,
                Ranking = movie.Ranking,
                RankedDateTime = movie.RankedDateTime
            };
        }

        public MovieDb ToMovieDbModel(int userId, MovieRankRequest movieRankRequest)
        {
            return new MovieDb
            {
                UserId = userId,
                MovieName = movieRankRequest.MovieName,
                Description = movieRankRequest.Description,
                Ranking = movieRankRequest.Ranking,
                Actors = movieRankRequest.Actors,
                RankedDateTime = DateTime.UtcNow.ToString()
            };
        }

        public MovieDb ToMovieDbModel(int userId, MovieDb response, MovieUpdateRequest request)
        {
            return new MovieDb
            {
                UserId = response.UserId,
                Description = response.Description,
                Actors = response.Actors,
                MovieName = response.MovieName,
                Ranking = request.Ranking,
                RankedDateTime = DateTime.UtcNow.ToString()
            };
        }
    }
}