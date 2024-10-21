using Microsoft.AspNetCore.Mvc;
using RookieApp.Models;
using System.Net;

namespace RookieApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {

        /// <summary>
        /// Метод для получения исполнителя по id
        /// </summary>
        /// <param name="id">Идегтификатор исполнителя</param>
        /// <returns>Возвращает название исполнителя</returns>
        [HttpGet(Name = "GetArtist")]
        public IEnumerable<Artist>? Get(int id)
        {
            var result = DataBaseAdapter.GetData(id); // Получаем данные из БД
            var list = new List<Artist>(); // Создаем пустой список
            if (result.Count == 0)
                return null;
            if (result.Count > 0) // Проверяем, есть ли в выборке из БД какие-то данные
            {
                list.Add( // Добавляем в список исполнителя
                new Artist 
                {
                    ID = result.Keys.First(), // Идентификатору исполнителя присваиваем ИД(получаем ключ из словаря, полученного из БД 
                    Name = result[result.Keys.First()] // Имени исполнител. присваиваем имя. 
                });
            }
            
            return [.. list]; // Возврашаем результат
        }

        /// <summary>
        /// Метод для добавления исполнителя
        /// </summary>
        /// <param name="ArtistName">Название исполнителя</param>
        /// <returns>Возвращает идентификатор и название добавленного исполнителя</returns>
        [HttpPost(Name = "AddArtist")]
        public IEnumerable<Artist> AddArtist(string ArtistName)
        {
            var result = DataBaseAdapter.AddData(ArtistName); // Отправляем данные в БД
            return
            [
                new() {
                    ID = result, // Присваиваем идентификатор из БД
                    Name = ArtistName // Присваиваем название из параметров
                }
            ];
        }
    }
}
