using Advent.Puzzles;
using System.Diagnostics.CodeAnalysis;

namespace AdventTests.Puzzles
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class Puzzle3Tests : PuzzleTestBase<Puzzle3>
    {
        [Test]
        [TestCase("day3_sample", ExpectedResult = "4361")]
        [TestCase("day3", ExpectedResult = "527446")]   // 524810 <> 528341
        public string SolveFirstPuzzle(string file)
        {
            return SolvePuzzle1(file);
        }

        [Test]
        [TestCase("day3_sample", ExpectedResult = "467835")]
        [TestCase("day3", ExpectedResult = "73201705")]
        public string SolveSecondPuzzle(string file)
        {
            return SolvePuzzle2(file);
        }
    }
}
