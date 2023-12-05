using Advent.Puzzles;
using System.Diagnostics.CodeAnalysis;

namespace AdventTests.Puzzles
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class Puzzle4Tests : PuzzleTestBase<Puzzle4>
    {
        [Test]
        [TestCase("day4_sample", ExpectedResult = "13")]
        [TestCase("day4", ExpectedResult = "21138")]
        public string SolveFirstPuzzle(string file)
        {
            return SolvePuzzle1(file);
        }

        [Test]
        [TestCase("day4_sample", ExpectedResult = "30")]
        [TestCase("day4", ExpectedResult = "7185540")]
        public string SolveSecondPuzzle(string file)
        {
            return SolvePuzzle2(file);
        }
    }
}
