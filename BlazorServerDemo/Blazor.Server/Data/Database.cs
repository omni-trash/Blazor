using System.Data;
using System.Data.SqlClient;

namespace Blazor.Server.Data;

public class Database
{
    public SqlConnection Connection { get; set; }

    public Database(SqlConnection connection)
    {
        Connection = connection;
    }

    public Database(string connectionString)
    {
        Connection = new SqlConnection(connectionString);
    }

    private List<DataRow> Execute(string commandText)
    {
        using (SqlCommand command = new SqlCommand(commandText, Connection))
        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
        {
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt.Rows.Cast<DataRow>().ToList();
        }
    }

    public List<T> Query<T>(string commandText) where T : class
    {
        return Execute(commandText).Select(DataRowConverter<T>.Cast).ToList();
    }

    public int ExecNonQuery(string commandText)
    {
        using (SqlCommand command = new SqlCommand(commandText, Connection))
        {
            return command.ExecuteNonQuery();
        }
    }
}
