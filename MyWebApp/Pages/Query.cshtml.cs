using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Data;               // for DataTable
using Microsoft.Data.Sqlite;     // for SQLite
using System.Text;
using System.Linq;

namespace MyWebApp.Pages
{
    [Authorize] // only logged-in users
    public class QueryModel : PageModel
    {
        [BindProperty]
        public string SqlQuery { get; set; } = "";

        public DataTable? QueryResults { get; set; }
        public string? ErrorMessage { get; set; }

        [TempData]
        public string LastQuery { get; set; } = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (string.IsNullOrWhiteSpace(SqlQuery))
            {
                ErrorMessage = "SQL Query cannot be empty.";
                return;
            }

            LastQuery = SqlQuery;
            RunQuery(SqlQuery);
        }

        public IActionResult OnGetExportCsv()
        {
            if (string.IsNullOrWhiteSpace(LastQuery))
            {
                ErrorMessage = "No query to export.";
                return RedirectToPage();
            }

            var dt = RunQuery(LastQuery);

            if (dt == null || dt.Rows.Count == 0)
            {
                ErrorMessage = "No data to export.";
                return RedirectToPage();
            }

            var csv = new StringBuilder();

            // Headers
            foreach (DataColumn col in dt.Columns)
            {
                csv.Append($"\"{col.ColumnName}\",");
            }
            csv.Length--;
            csv.AppendLine();

            // Rows
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    csv.Append($"\"{item}\",");
                }
                csv.Length--;
                csv.AppendLine();
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray();
            return File(bytes, "text/csv", "QueryResults.csv");
        }

        private DataTable RunQuery(string query)
        {
            try
            {
                using var connection = new SqliteConnection("Data Source=app.db");
                connection.Open();

                using var command = new SqliteCommand(query, connection);
                using var reader = command.ExecuteReader();

                var dt = new DataTable();
                dt.Load(reader);

                QueryResults = dt;
                return dt;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
