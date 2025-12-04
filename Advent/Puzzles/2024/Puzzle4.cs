using Advent.IO;
using System.Text;

namespace Advent.Puzzles._2024
{
    internal class Puzzle4 : IPuzzle
    {
        private const string wordToFind = "XMAS";

        public int Year { get; set; }

        public string SolvePuzzle1(string file)
        {
            var lines = FileParser.ReadInputFileAsLines(file, Year);

            var total = FindWordOccurenceHorizontally(lines);
            total += FindWordOccurenceVertically(lines);
            total += FindWordOccurenceDiagonally(lines);

            lines.Reverse();

            total += FindWordOccurenceHorizontally(lines);
            total += FindWordOccurenceVertically(lines);
            total += FindWordOccurenceDiagonally(lines);

            return total.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            throw new NotImplementedException();
        }

        private int FindWordOccurenceHorizontally(List<string> lines)
        {
            var count = 0;

            foreach (var line in lines)
            {
                var normal = line;

                count += GetTotalWordOccurenceInLine(normal);
            }

            return count;
        }

        private int FindWordOccurenceVertically(List<string> lines)
        {
            var count = 0;

            var vertical = new List<string>();

            for (var i=0; i<lines.First().Length; i++)
            {
                var stringBuilder = new StringBuilder();

                for (var j=0; j<lines.Count; j++)
                {
                    stringBuilder.Append(lines[j].Substring(i, 1));
                }

                vertical.Add(stringBuilder.ToString());
            }

            foreach (var line in vertical)
            {
                var normal = line;

                count += GetTotalWordOccurenceInLine(normal);
            }

            return count;
        }

        private int FindWordOccurenceDiagonally(List<string> lines)
        {
            var count = 0;

            for (var i=0; i<lines.Count; i++)
            {
                var currentLetter = wordToFind.First();

                var indexOfLetter = lines[i].IndexOf(currentLetter);

                if (indexOfLetter > -1)
                {

                }
            }

            return count;
        }

        private int GetTotalWordOccurenceInLine(string line)
        {
            var count = 0;

            var index = line.IndexOf(wordToFind);

            while (index > -1)
            {
                count++;
                line = line.Substring(index + wordToFind.Length);
                index = line.IndexOf(wordToFind);
            }

            return count;
        }
    }
}
