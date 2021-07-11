using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories
{
    public class MovieRankRepository : IMovieRankRepository
    {
        private readonly IDynamoDBContext context;
        public MovieRankRepository(IAmazonDynamoDB dynamoDbClient)
        {
            //AWSCredentials credential;
            //AmazonDynamoDBClient client = new AmazonDynamoDBClient(credential);
            this.context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task AddMovie(MovieDb movieDb)
        {
            await context.SaveAsync<MovieDb>(movieDb);
        }

        public async Task<IEnumerable<MovieDb>> GetAllItems()
        {
            return await this.context.ScanAsync<MovieDb>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<MovieDb> GetMovie(int userId, string movieName)
        {
            return await this.context.LoadAsync<MovieDb>(userId, movieName);
        }

        public async Task<IEnumerable<MovieDb>> GetMovieRank(string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "MovieName-Index"
            };
            return await context.QueryAsync<MovieDb>(movieName, config).GetRemainingAsync();
        }

        public async Task<IEnumerable<MovieDb>> GetUserRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("MovieName", ScanOperator.BeginsWith, movieName)
                }
            };
            return await context.QueryAsync<MovieDb>(userId, config).GetRemainingAsync();
        }

        public async Task UpdateMovie(MovieDb movieDb)
        {
            await context.SaveAsync(movieDb);
        }
    }
}
