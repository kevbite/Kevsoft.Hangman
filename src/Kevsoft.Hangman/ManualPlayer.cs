using System;
using System.Threading.Tasks;

namespace Kevsoft.Hangman
{
    public class ManualPlayer
    {
        private readonly IPlayableGame _game;

        public ManualPlayer(IPlayableGame game)
        {
            _game = game;
        }

        public async Task PlayAsync()
        {
            for (; ; )
            {
                var letter = Console.ReadKey().KeyChar;
                await _game.PlayTurn(letter)
                    .ConfigureAwait(false);

                Console.Clear();
                Console.WriteLine($"Id: {_game.State.Id}");
                Console.WriteLine($"Completed: {_game.State.Complete}");
                Console.WriteLine($"LettersGuessed: {string.Join(",", _game.State.LettersGuessed)}");
                Console.WriteLine($"Progress: {string.Join(",", _game.State.Progress)}");
                Console.WriteLine($"Misses: {string.Join(",", _game.State.Misses)}");
            }
        }
    }
}