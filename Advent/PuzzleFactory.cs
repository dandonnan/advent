using Advent.Puzzles;

namespace Advent
{
    internal class PuzzleFactory
    {
        public static IPuzzle GetPuzzle(int day, out string file)
        {
            file = $"day{day}";

            return (IPuzzle)Activator.CreateInstance("Advent", $"Advent.Puzzles.Puzzle{day}").Unwrap();
        }
    }
}
