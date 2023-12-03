
using Advent.Puzzles;

const string solutionFormat = "The solution to the {0} puzzle is {1}.";

IPuzzle puzzle = new Puzzle1();

string file = "day1";

Console.WriteLine(string.Format(solutionFormat, "first", puzzle.SolvePuzzle1(file)));
Console.WriteLine(string.Format(solutionFormat, "second", puzzle.SolvePuzzle2(file)));