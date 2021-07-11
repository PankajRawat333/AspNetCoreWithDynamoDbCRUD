using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System.Collections.Generic;

namespace MovieRank.Libs.Mappers
{
    public interface IMapper
    {
        IEnumerable<MovieResponse> ToMovieContract(IEnumerable<MovieDb> items);

        MovieResponse ToMovieContract(MovieDb item);
        
        MovieDb ToMovieDbModel(int userId, MovieRankRequest movieRankRequest);

        MovieDb ToMovieDbModel(int userId, MovieDb response, MovieUpdateRequest request);
    }
}