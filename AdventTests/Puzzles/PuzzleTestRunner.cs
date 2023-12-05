using Advent;
using NUnit.Framework.Internal;
using System.Diagnostics.CodeAnalysis;

namespace AdventTests.Puzzles
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class PuzzleTestRunner
    {
        [Test]
        [TestCaseSource(nameof(GetTests))]
        public void SolvePuzzle1(PuzzleTestModel test)
        {
            string testFile = test.InputFile;

            // If the input file is split for each puzzle, use the correct one
            if (test.SplitInputs)
            {
                testFile = $"{testFile}1";
            }

            // Test that the solution to the first puzzle matches the expected result
            Assert.That(test.Puzzle1ExpectedResult, Is.EqualTo(test.Puzzle.SolvePuzzle1(testFile)));
        }

        [Test]
        [TestCaseSource(nameof(GetTests))]
        public void SolvePuzzle2(PuzzleTestModel test)
        {
            string testFile = test.InputFile;

            // If the input file is split for each puzzle, use the correct one
            if (test.SplitInputs)
            {
                testFile = $"{testFile}2";
            }

            // Test that the solution to the second puzzle matches the expected result
            Assert.That(test.Puzzle2ExpectedResult, Is.EqualTo(test.Puzzle.SolvePuzzle2(testFile)));
        }

        // A list of tests to run with their expected inputs
        private static List<PuzzleTestModel> GetTests()
        {
            return new List<PuzzleTestModel>
            {
                // Samples
                CreateTestModel(1, "142", "281", true, true),
                CreateTestModel(2, "8", "2286", true),
                CreateTestModel(3, "4361", "467835", true),
                CreateTestModel(4, "13", "30", true),

                // Puzzles
                CreateTestModel(1, "54632", "54019"),
                CreateTestModel(2, "2685", "83707"),
                CreateTestModel(3, "527446", "73201705"),
                CreateTestModel(4, "21138", "7185540")
            };
        }

        private static PuzzleTestModel CreateTestModel(int day, string expectedResult1, string expectedResult2, bool sample = false, bool splitInputs = false)
        {
            var puzzle = PuzzleFactory.GetPuzzle(day, out string file);

            return new PuzzleTestModel
            {
                Puzzle = puzzle,
                InputFile = sample ? $"{file}_sample" : file,
                Puzzle1ExpectedResult = expectedResult1,
                Puzzle2ExpectedResult = expectedResult2,
                SplitInputs = splitInputs
            };
        }
    }
}
