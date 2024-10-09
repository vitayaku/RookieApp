using Microsoft.AspNetCore.Mvc;
using RookieApp.Controllers;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ArtistController> _logger;

        public ArtistController(ILogger<ArtistController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetArtist")]
        public IEnumerable<Artist> Get(int id)
        {
            var result = DataBaseAdapter.GetData(id);
            var list = new List<Artist>();
            if (result.Any()) 
            {
                list.Add(
                new Artist
                {
                    ID = result.Keys.First(),
                    Name = result[result.Keys.First()]
                });
            }
            return list.ToArray();
        }

        [HttpPost(Name = "AddArtist")]
        public IEnumerable<Artist> AddArtist(string ArtistName)
        {
            var result = DataBaseAdapter.AddData(ArtistName);
            return new List<Artist>
            {
                new Artist
                {
                    ID = result,
                    Name = ArtistName
                }
            }
            .ToArray();
        }
    }
}
