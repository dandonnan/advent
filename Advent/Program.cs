
using Advent;
using Advent.Puzzles;

const string solutionFormat = "The solution to the {0} puzzle is {1}.";

int day = 1;

int year = 2024;

IPuzzle puzzle = PuzzleFactory.GetPuzzle(day, year, out string file);

Console.WriteLine(string.Format(solutionFormat, "first", puzzle.SolvePuzzle1(file)));
Console.WriteLine(string.Format(solutionFormat, "second", puzzle.SolvePuzzle2(file)));

Console.ReadLine();