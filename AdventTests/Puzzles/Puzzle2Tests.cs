using Advent.Puzzles;
using System.Diagnostics.CodeAnalysis;

namespace AdventTests.Puzzles
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class Puzzle2Tests : PuzzleTestBase<Puzzle2>
    {
        [Test]
        [TestCase("day2_sample", ExpectedResult = "8")]
        [TestCase("day2", ExpectedResult = "2685")]
        public string SolveFirstPuzzle(string file)
        {
            return SolvePuzzle1(file);
        }

        [Test]
        [TestCase("day2_sample", ExpectedResult = "2286")]
        [TestCase("day2", ExpectedResult = "83707")]
        public string SolveSecondPuzzle(string file)
        {
            return SolvePuzzle2(file);
        }
    }
}
