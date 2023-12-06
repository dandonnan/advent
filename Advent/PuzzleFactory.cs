using Advent.Puzzles;

namespace Advent
{
    internal class PuzzleFactory
    {
        public static IPuzzle GetPuzzle(int day, int year, out string file)
        {
            file = $"day{day}";

            var puzzle = (IPuzzle)Activator.CreateInstance("Advent", $"Advent.Puzzles._{year}.Puzzle{day}").Unwrap();

            puzzle.Year = year;

            return puzzle;
        }
    }
}
