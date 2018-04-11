using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kevsoft.Hangman
{
    public class Game : IPlayableGame
    {
        private readonly string _token;

        private static readonly HttpClient HttpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://dojo-hangman-server.herokuapp.com/api/")
        };

        public GameState State { get; private set; }

        public Game(string token)
        {
            _token = token;
        }
        
        public async Task PlayTurn(char letter)
        {
            var content = $@"{{""letter"": ""{letter}""}}";

            var httpRequestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), "games/current")
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            HttpClient.DefaultRequestHeaders.Remove("x-access-token");
            HttpClient.DefaultRequestHeaders.Add("x-access-token", _token);

            var httpResponseMessage = await HttpClient.SendAsync(httpRequestMessage);
            var body = await httpResponseMessage.Content.ReadAsStringAsync();

            State = JsonConvert.DeserializeObject<GameState>(body);
        }
    }
}