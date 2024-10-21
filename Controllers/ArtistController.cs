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
        /// ����� ��� ��������� ����������� �� id
        /// </summary>
        /// <param name="id">������������� �����������</param>
        /// <returns>���������� �������� �����������</returns>
        [HttpGet(Name = "GetArtist")]
        public IEnumerable<Artist>? Get(int id)
        {
            var result = DataBaseAdapter.GetData(id); // �������� ������ �� ��
            var list = new List<Artist>(); // ������� ������ ������
            if (result.Count == 0)
                return null;
            if (result.Count > 0) // ���������, ���� �� � ������� �� �� �����-�� ������
            {
                list.Add( // ��������� � ������ �����������
                new Artist 
                {
                    ID = result.Keys.First(), // �������������� ����������� ����������� ��(�������� ���� �� �������, ����������� �� �� 
                    Name = result[result.Keys.First()] // ����� ����������. ����������� ���. 
                });
            }
            
            return [.. list]; // ���������� ���������
        }

        /// <summary>
        /// ����� ��� ���������� �����������
        /// </summary>
        /// <param name="ArtistName">�������� �����������</param>
        /// <returns>���������� ������������� � �������� ������������ �����������</returns>
        [HttpPost(Name = "AddArtist")]
        public IEnumerable<Artist> AddArtist(string ArtistName)
        {
            var result = DataBaseAdapter.AddData(ArtistName); // ���������� ������ � ��
            return
            [
                new() {
                    ID = result, // ����������� ������������� �� ��
                    Name = ArtistName // ����������� �������� �� ����������
                }
            ];
        }
    }
}
