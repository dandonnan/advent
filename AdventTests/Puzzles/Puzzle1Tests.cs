using Advent.Puzzles;
using System.Diagnostics.CodeAnalysis;

namespace AdventTests.Puzzles
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class Puzzle1Tests : PuzzleTestBase<Puzzle1>
    {
        [Test]
        [TestCase("day1_sample1", ExpectedResult = "142")]
        [TestCase("day1", ExpectedResult = "54632")]
        public string SolveFirstPuzzle(string file)
        {
            return SolvePuzzle1(file);
        }

        [Test]
        [TestCase("day1_sample2", ExpectedResult = "281")]
        [TestCase("day1", ExpectedResult = "54019")]
        public string SolveSecondPuzzle(string file)
        {
            return SolvePuzzle2(file);
        }
    }
}
