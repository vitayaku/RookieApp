namespace RookieApp.Models
{
    /// <summary>
    /// Модель данных для исполнителя
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public required string ID { get; set; }
        /// <summary>
        /// Имя исполнителя
        /// </summary>
        public required string Name { get; set; }
    }
}
