using Advent.Puzzles;

namespace Advent
{
    internal class PuzzleFactory
    {
        public static IPuzzle GetPuzzle(int day, out string file)
        {
            file = $"day{day}";

            switch (day)
            {
                case 1:
                    return new Puzzle1();

                case 2:
                    return new Puzzle2();

                default:
                    return null;
            }
        }
    }
}
