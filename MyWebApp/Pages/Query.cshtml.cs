using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Microsoft.Data.Sqlite;

namespace MyWebApp.Pages
{
    public class QueryModel : PageModel
    {
        public string SqlQuery { get; set; } = "";
        public DataTable? QueryResults { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public void OnPost(string sqlQuery)
        {
            SqlQuery = sqlQuery;

            try
            {
                using var connection = new SqliteConnection("Data Source=app.db");
                connection.Open();

                using var command = new SqliteCommand(SqlQuery, connection);
                using var reader = command.ExecuteReader();

                var dt = new DataTable();
                dt.Load(reader);

                QueryResults = dt;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
