using Dapper;
using Infrastructure.models;
using Npgsql;

namespace Infrastructure;

public class Repository
{
    private readonly NpgsqlDataSource _dataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Book> getALlBooks()
    {
        var sql = $@"SELECT * FROM library.books;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Book>(sql);
        }
    }
}