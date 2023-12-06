namespace Advent.Puzzles
{
    internal interface IPuzzle
    {
        int Year { get; set; }

        string SolvePuzzle1(string file);

        string SolvePuzzle2(string file);
    }
}
