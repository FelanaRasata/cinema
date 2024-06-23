using backoffice.Models;
using Microsoft.Data.SqlClient;

namespace frontoffice.Database;

public class SessionService
{
    private readonly string? _connectionString;

    public SessionService(string? connectionString)
    {
        _connectionString = connectionString;
    }

    private List<Session> FindSessions(string query)
    {
        var sessions = new List<Session>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Session session = new Session
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("SessionId")),
                        MovieId = reader.GetInt32(reader.GetOrdinal("MovieId")),
                        SessionDateTime = reader.GetDateTime(reader.GetOrdinal("SessionDateTime")),
                        Room = reader.GetInt32(reader.GetOrdinal("Room")),
                        Movie = new Movie
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("MovieId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            Category = reader.GetString(reader.GetOrdinal("Category"))
                        }
                    };

                    sessions.Add(session);
                }
            }
        }

        return sessions;
    }


    public int GetTotalSessionCount()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT COUNT(*) FROM Sessions";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
    }

    public List<Session> GetSessionsWithMovies(int pageNumber, int pageSize)
    {
        string sql =
            $" SELECT s.Id AS SessionId, s.MovieId, s.SessionDateTime, s.Room, m.Id AS MovieId, m.Title, m.Description, m.Duration, m.Category FROM Sessions s JOIN Movies m ON s.MovieId = m.Id ORDER BY s.Id OFFSET {pageNumber} * ({pageNumber} - 1) ROWS FETCH NEXT {pageSize} ROWS ONLY";
        List<Session> sessions = FindSessions(sql);

        return sessions;
    }

    public List<Session> GetSessionsByMovie(int movieId)
    {
        string sql =
            $" SELECT s.Id AS SessionId, s.MovieId, s.SessionDateTime, s.Room, m.Id AS MovieId, m.Title, m.Description, m.Duration, m.Category FROM Sessions s JOIN Movies m ON s.MovieId = m.Id WHERE s.MovieId ={movieId}";
        List<Session> sessions = FindSessions(sql);

        return sessions;
    }

    public Session GetSessionsById(int id)
    {
        string sql =
            $" SELECT s.Id AS SessionId, s.MovieId, s.SessionDateTime, s.Room, m.Id AS MovieId, m.Title, m.Description, m.Duration, m.Category FROM Sessions s JOIN Movies m ON s.MovieId = m.Id WHERE s.Id ={id}";
        List<Session> sessions = FindSessions(sql);

        return sessions[0];
    }
}