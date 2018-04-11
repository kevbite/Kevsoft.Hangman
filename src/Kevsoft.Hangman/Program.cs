using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kevsoft.Hangman
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjVhY2U1NjhlNWU3OTM4MDAxNDBhMjYyMyIsImlhdCI6MTUyMzQ3MjAxNCwiZXhwIjoxNTIzNTU4NDE0fQ.VrJHqwisGAwXoqRLaNGf8b3MFUSDsO5iOQdK_1iH-n4";

            var game = new Game(token);

            var bot = new BotPlayer(game);
            await bot.PlayAsync()
                .ConfigureAwait(false);

            var human = new ManualPlayer(game);
            await human.PlayAsync()
                .ConfigureAwait(false);
        }


        private static async Task<object> Register(string username, string password)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://dojo-hangman-server.herokuapp.com/api")
            };

            httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            var serializeObject = JsonConvert.SerializeObject(new {username, password });
            var stringContent = new StringContent(serializeObject);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpResponseMessage = await httpClient.PostAsync("/auth/register",
                stringContent).ConfigureAwait(false);

            httpResponseMessage.EnsureSuccessStatusCode();

            var body = await httpResponseMessage.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            var deserializeObject = JsonConvert.DeserializeObject<dynamic>(body);

            return (string)deserializeObject.token;
        }
    }
}
