using System.Threading.Tasks;

namespace Kevsoft.Hangman
{
    public interface IPlayableGame
    {
        GameState State { get; }

        Task PlayTurn(char letter);
    }
}