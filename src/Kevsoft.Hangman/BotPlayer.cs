using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kevsoft.Hangman
{
    public class BotPlayer
    {
        private static readonly string[] Words = File.ReadAllLines(@".\words.txt");

        public IPlayableGame Game { get; }

        public BotPlayer(IPlayableGame game)
        {
            Game = game;
        }

        public async Task PlayAsync()
        {
            await Game.PlayTurn('e')
                .ConfigureAwait(false);
            
            while (!Game.State.Complete)
            {
                var cutDownWords = GetWordsByLength(Game.State.Progress.Length);
                var missedLetters = Game.State.LettersGuessed.Where(x => !Game.State.Progress.Contains(x))
                    .Select(x => x[0]);

                cutDownWords = cutDownWords.Where(x => !x.Any(c => missedLetters.Contains(c))).ToArray();

                var nextLetter = string.Join("", cutDownWords)
                    .GroupBy(x => x)
                    .OrderByDescending(x => x.Count())
                    .First(x => !Game.State.Progress.Where(y => y != null).Select(y => y[0]).Contains(x.Key)).Key;

                await Game.PlayTurn(nextLetter)
                    .ConfigureAwait(false);
            }
        }

        private static string[] GetWordsByLength(int progressLength)
        {
            return Words.Where(x => x.Length == progressLength).ToArray();
        }
    }
}