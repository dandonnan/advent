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
            Assert.That(test.Puzzle.SolvePuzzle1(testFile), Is.EqualTo(test.Puzzle1ExpectedResult));
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
            Assert.That(test.Puzzle.SolvePuzzle2(testFile), Is.EqualTo(test.Puzzle2ExpectedResult));
        }

        // A list of tests to run with their expected inputs
        private static List<PuzzleTestModel> GetTests()
        {
            return new List<PuzzleTestModel>
            {
                // 2023

                // Samples
                CreateTestModel(1, 2023, "142", "281", true, true),
                CreateTestModel(2, 2023, "8", "2286", true),
                CreateTestModel(3, 2023, "4361", "467835", true),
                CreateTestModel(4, 2023, "13", "30", true),
                CreateTestModel(6, 2023, "288", "71503", true),

                // Puzzles
                CreateTestModel(1, 2023, "54632", "54019"),
                CreateTestModel(2, 2023, "2685", "83707"),
                CreateTestModel(3, 2023, "527446", "73201705"),
                CreateTestModel(4, 2023, "21138", "7185540"),
                CreateTestModel(6, 2023, "345015", "42588603"),

                // 2022

                // Samples
                CreateTestModel(1, 2022, "24000", "45000", true),
                CreateTestModel(2, 2022, "15", "12", true),
                CreateTestModel(3, 2022, "157", "70", true),
                CreateTestModel(4, 2022, "2", "4", true),
                CreateTestModel(5, 2022, "CMZ", "MCD", true),
                // todo: add support for more than 2 sample files

                // Puzzles
                CreateTestModel(1, 2022, "66719", "198551"),
                CreateTestModel(2, 2022, "10624", "14060"),
                CreateTestModel(3, 2022, "8018", "2518"),
                CreateTestModel(4, 2022, "515", "883"),
                CreateTestModel(5, 2022, "SBPQRSCDF", "RGLVRCQSB"),
                CreateTestModel(6, 2022, "1566", "2265"),
            };
        }

        private static PuzzleTestModel CreateTestModel(int day, int year, string expectedResult1, string expectedResult2, bool sample = false, bool splitInputs = false)
        {
            var puzzle = PuzzleFactory.GetPuzzle(day, year, out string file);

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
