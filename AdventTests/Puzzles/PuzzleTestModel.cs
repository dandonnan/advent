using Advent.Puzzles;

namespace AdventTests.Puzzles
{
    internal class PuzzleTestModel
    {
        // The puzzle to test
        public IPuzzle Puzzle { get; set; }

        // The name of the input file
        public string InputFile { get; set; }

        // The expected result for puzzle 1
        public string Puzzle1ExpectedResult { get; set; }

        // The expected result for puzzle 2
        public string Puzzle2ExpectedResult { get; set; }

        // Whether the input files are different for each puzzle
        public bool SplitInputs { get; set; }

        public override string ToString()
        {
            return InputFile;
        }
    }
}
