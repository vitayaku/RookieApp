using System.Data;
using System.Data.SQLite;
// Здесь подключаем пространства имен(библиотеки для работы с кодом)

namespace RookieApp.Controllers // Пространства имен используем для удобства работы с именами подробнее см в https://ru.wikipedia.org/wiki/%D0%A1%D0%B8%D0%BD%D1%82%D0%B0%D0%BA%D1%81%D0%B8%D1%81_%D1%8F%D0%B7%D1%8B%D0%BA%D0%B0_C_Sharp
{
    public class DataBaseAdapter // Класс для работы с БД
    {
        /// <summary>
        /// Метод для добавления данных в БД
        /// </summary>
        /// <param name="param">Название исполнителя</param>
        /// <returns></returns>
        public static string AddData(string param) 
        {
            string? resultExecution; // Создаем пустую переменную для результата
            using (var connection = new SQLiteConnection("Data Source=chinook.db")) // Указываем путь к БД
            {
                connection.Open();// Открываем соединение
                using (SQLiteCommand command = new(connection))

                {

                    command.CommandText = $"INSERT INTO artists (ArtistId, Name) VALUES ((select max(ArtistId) + 1  from artists), '{param}')";// Прописываем запрос на вставку
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery(); // Выполняем запрос

                }


                using (SQLiteCommand command = new(connection))
                {

                    command.CommandText = $"select max(ArtistId) from artists"; // Выбираем полученный ID
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    string? result = string.Empty;
                    while (reader.Read())
                    {
                        result = reader.GetValue(0).ToString(); // Присваиваем поллученныее данные результату
                    }
                    resultExecution = result ?? null;
                    reader.Close();
                }
                connection.Close();// Закрываем соединение
            }
            return resultExecution ?? "Ошибка"; //Возвращаем результат
            
        }
        /// <summary>
        /// Возвращает исполнителя по ID. Логика аналогична методу выше
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
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
