using System.Data;
using System.Data.SQLite;


namespace RookieApp.Controllers
{
    public class DataBaseAdapter
    {
        public static string AddData(string param)
        {
            string? resultExecution;
            using (var connection = new SQLiteConnection("Data Source=chinook.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new(connection))

                {

                    command.CommandText = $"INSERT INTO artists (ArtistId, Name) VALUES ((select max(ArtistId) + 1  from artists), '{param}')";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                }


                using (SQLiteCommand command = new(connection))
                {

                    command.CommandText = $"select max(ArtistId) from artists";
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    string? result = string.Empty;
                    while (reader.Read())
                    {
                        result = reader.GetValue(0).ToString();
                    }
                    resultExecution = result ?? null;
                    reader.Close();
                }
                Console.WriteLine();
                connection.Close();
            }
            return resultExecution ?? "Ошибка";
            
        }

        public static Dictionary<string, string> GetData(int param)
        {
            Dictionary<string, string> result = [];
            using (var connection = new SQLiteConnection("Data Source=chinook.db"))
            {
                connection.Open();
                

                using (SQLiteCommand command = new(connection))
                {

                    command.CommandText = $"Select a.artistid, a.name FROM artists a where a.artistid = {param}";
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string? key = reader.GetValue(0).ToString();
                        string? value = reader.GetValue(1).ToString();
                        if(key is not null && value is not null)
                            result.Add(key, value);
                    }
                    reader.Close();
                }
                Console.WriteLine();
                connection.Close();
            }
            return result;

        }
    }
}
