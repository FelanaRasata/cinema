using backoffice.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace frontoffice.Database;

public class MovieService
{
    private readonly string? _connectionString;

    public MovieService(string? connectionString)
    {
        _connectionString = connectionString;
    }

    private List<Movie> FindMovies(string query)
    {
        var movies = new List<Movie>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    movies.Add(new Movie
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Description = (string)reader["Description"],
                        Duration = (int)reader["Duration"],
                        Category = (string)reader["Category"]
                    });
                }
            }
        }

        return movies;
    }

    public List<Movie> GetMovies(int pageNumber, int pageSize, string? search)
    {
        int offset = (pageNumber - 1) * pageSize;

        string query = "SELECT * FROM Movies ";

        if (!search.IsNullOrEmpty())
        {
            query +=
                $" WHERE Title LIKE '%{search}%' OR Description LIKE '%{search}%' OR Category LIKE '%{search}%' ";
        }


        query += $" ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;";

        var movies = FindMovies(query);
        return movies;
    }

    public Movie? GetMovie(int id)
    {
        string query = $"SELECT * FROM Movies WHERE Id={id}";
        var movies = FindMovies(query);
        return movies[0];
    }

    /*public List<Movie> SearchMovie(string search)
    {
        string query =
            $"SELECT * FROM Movies WHERE Title LIKE '%{search}%' Description LIKE '%{search}%' Category LIKE '%{search}%' ";
        var movies = FindMovies(query);
        return movies;
    }*/

    public int CountMovies(string search)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT COUNT(*) FROM Movies";
            
            if (!search.IsNullOrEmpty())
            {
                sql +=
                    $" WHERE Title LIKE '%{search}%' OR Description LIKE '%{search}%' OR Category LIKE '%{search}%' ";
            }

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
    }

    public List<Movie> GetAllMovies()
    {
        string query = "SELECT * FROM Movies";

        var movies = FindMovies(query);
        return movies;
    }
}