using Microsoft.AspNetCore.Mvc;
using RookieApp.Controllers;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackPlayerController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TrackPlayerController> _logger;

        public TrackPlayerController(ILogger<TrackPlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetArtist")]
        public IEnumerable<TrackPlayer> Get(int id)
        {
            var result = DataBaseAdapter.GetData(id);
            var list = new List<TrackPlayer>();
            if (result.Any()) 
            {
                list.Add(
                new TrackPlayer
                {
                    ComposerID = result.Keys.First(),
                    ComposerName = result[result.Keys.First()]
                });
            }
            return list.ToArray();
        }

        [HttpPost(Name = "AddArtist")]
        public IEnumerable<TrackPlayer> AddArtist(string ArtistName)
        {
            var result = DataBaseAdapter.AddData(ArtistName);
            return new List<TrackPlayer>
            {
                new TrackPlayer
                {
                    ComposerID = result,
                    ComposerName = ArtistName
                }
            }
            .ToArray();
        }
    }
}
