using Advent.IO;

namespace Advent.Puzzles._2023
{
    internal class Puzzle9 : IPuzzle
    {
        public int Year { get; set; }

        public string SolvePuzzle1(string file)
        {
            long sum = 0;

            var lines = FileParser.ReadInputFileAsLines(file, Year);

            foreach (var line in lines)
            {
                // Get the numbers in the line
                var history = GetHistoryForLine(line);

                // Find the next value in the sequence and add to the final sum
                sum += GetNextInSequence(history);
            }

            return sum.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            long sum = 0;

            var lines = FileParser.ReadInputFileAsLines(file, Year);

            foreach (var line in lines)
            {
                // Get the numbers in the line
                var history = GetHistoryForLine(line);

                // Find the first value in the sequence and add to the final sum
                sum += GetFirstInSequence(history);
            }

            return sum.ToString();
        }

        private static List<long> GetHistoryForLine(string line)
        {
            var history = new List<long>();

            // Split each value in the line
            var split = line.Split(" ");

            // Convert the values into numbers to add to the list
            foreach (var number in split)
            {
                if (string.IsNullOrWhiteSpace(number) == false)
                {
                    history.Add(long.Parse(number));
                }
            }

            return history;
        }

        private static long GetNextInSequence(List<long> history)
        {
            List<long> difference = new List<long>();

            // Go through the line and calculate the differences between
            // neighbouring values
            for (int i=0; i<history.Count-1; i++)
            {
                difference.Add(history[i + 1] - history[i]);
            }

            // If all the differences are not 0
            if (difference.All(d => d == 0) == false)
            {
                // Work out the next in the sequence for the differences
                var next = GetNextInSequence(difference);

                // Update the sequence to include the next value
                history.Add(history.Last() + next);
            }

            return history.Last();
        }

        private static long GetFirstInSequence(List<long> history)
        {
            List<long> difference = new List<long>();

            // Go through the line and calculate the differences between
            // neighbouring values
            for (int i = 0; i < history.Count - 1; i++)
            {
                difference.Add(history[i + 1] - history[i]);
            }

            // If all the differences are not 0
            if (difference.All(d => d == 0) == false)
            {
                // Work out the first in the sequence for the differences
                var first = GetFirstInSequence(difference);

                // Update the sequence to include the first value
                history.Insert(0, history.First() - first);
            }

            return history.First();
        }
    }
}
