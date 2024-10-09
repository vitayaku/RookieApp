using Microsoft.AspNetCore.Mvc;
using RookieApp.Models;

namespace RookieApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {

        [HttpGet(Name = "GetArtist")]
        public IEnumerable<Artist> Get(int id)
        {
            var result = DataBaseAdapter.GetData(id);
            var list = new List<Artist>();
            if (result.Count > 0)
            {
                list.Add(
                new Artist
                {
                    ID = result.Keys.First(),
                    Name = result[result.Keys.First()]
                });
            }
            return [.. list];
        }

        [HttpPost(Name = "AddArtist")]
        public IEnumerable<Artist> AddArtist(string ArtistName)
        {
            var result = DataBaseAdapter.AddData(ArtistName);
            return
            [
                new() {
                    ID = result,
                    Name = ArtistName
                }
            ];
        }
    }
}
