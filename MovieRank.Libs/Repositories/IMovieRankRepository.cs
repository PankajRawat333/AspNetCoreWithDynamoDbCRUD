using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories
{
    public interface IMovieRankRepository
    {
        Task<IEnumerable<MovieDb>> GetAllItems();

        Task<MovieDb> GetMovie(int userId, string movieName);

        Task<IEnumerable<MovieDb>> GetUserRankedMoviesByMovieTitle(int userId, string movieName);

        Task AddMovie(MovieDb movieDb);
        Task UpdateMovie(MovieDb movieDb);

        Task<IEnumerable<MovieDb>> GetMovieRank(string movieName);
    }
}
