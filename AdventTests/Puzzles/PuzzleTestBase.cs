using Advent.Puzzles;
using System.Diagnostics.CodeAnalysis;

namespace AdventTests.Puzzles
{
    [ExcludeFromCodeCoverage]
    internal class PuzzleTestBase<T>
        where T : IPuzzle, new()
    {
        private T puzzle;

        [SetUp]
        public void OnTestStart()
        {
            puzzle = new T();
        }

        protected string SolvePuzzle1(string file)
        {
            return puzzle.SolvePuzzle1(file);
        }

        protected string SolvePuzzle2(string file)
        {
            return puzzle.SolvePuzzle2(file);
        }
    }
}
