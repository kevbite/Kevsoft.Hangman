using Newtonsoft.Json;

namespace Kevsoft.Hangman
{
    public class GameState
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lettersGuessed")]
        public string[] LettersGuessed { get; set; }

        [JsonProperty("progress")]
        public string[] Progress { get; set; }

        [JsonProperty("misses")]
        public int Misses { get; set; }

        [JsonProperty("complete")]
        public bool Complete { get; set; }
    }
}