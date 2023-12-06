using Advent.IO;

namespace Advent.Puzzles._2022
{
    // Day 1 Puzzle
    // Fairly happy with how this one turned out
    // although I'm now not sure if it is calory
    // or calorie - the second looks more correct
    // but still feels wrong
    internal class Puzzle1 : IPuzzle
    {
        public int Year { get; set; }

        // Find the amount of calories held by the elf that
        // has the most calories
        public string SolvePuzzle1(string file)
        {
            // Get the total amount of calories for each elf
            List<int> totals = GetCalorieTotals(file);

            // Order the totals in descending order, and grab
            // the first one (this will be the highest)
            int highest = totals.OrderByDescending(t => t)
                                .First();

            // Return the highest total as the answer
            return highest.ToString();
        }

        // Find the total calories of the three elves with
        // the highest calorie totals
        public string SolvePuzzle2(string file)
        {
            // Get the total amount of calories for each elf
            List<int> totals = GetCalorieTotals(file);

            // Order the totals in descending order, then grab
            // the first three (these will be the highest)
            List<int> topThree = totals.OrderByDescending(t => t)
                                       .Take(3)
                                       .ToList();

            // Track the combined totals
            int combined = 0;

            // Go through the top three totals
            foreach (int total in topThree)
            {
                // Add the total to the combined total
                combined += total;
            }

            // Return the combined total as the answer
            return combined.ToString();
        }

        // Get a list of the total number of calories for each elf
        private List<int> GetCalorieTotals(string file)
        {
            // Create a blank totals list
            List<int> totals = new List<int>();

            // Get all the lines from the file
            List<string> input = FileParser.ReadInputFileAsLines(file, Year);

            // Create a tracker for the current total
            int currentTotal = 0;

            // Go through each line
            foreach (string line in input)
            {
                // If there is data on the line
                if (string.IsNullOrWhiteSpace(line) == false)
                {
                    // Parse the line as an int
                    int number = int.Parse(line);

                    // Add the line's value to the current total
                    currentTotal += number;
                }
                else
                {
                    // If there is no data on a line, it
                    // indicates the end of an elf which
                    // means the current total is that
                    // elf's total - add this to the list
                    totals.Add(currentTotal);

                    // Reset the current total to start
                    // from 0 for the next elf
                    currentTotal = 0;
                }
            }

            // If the end of the file has been reached
            // and the last total has not been added
            if (currentTotal > 0)
            {
                // Add it
                totals.Add(currentTotal);
            }

            // Return the list
            return totals;
        }

        // This is the original solution to puzzle 1, before
        // optimising to reuse code with puzzle 2
        private string SolvePuzzle1_Original()
        {
            // Get all calories
            List<string> input = FileParser.ReadInputFileAsLines("day1", 2022);

            int highestTotal = 0;
            int currentTotal = 0;

            // Go through each line
            foreach (string line in input)
            {
                // If the line has data on it
                if (string.IsNullOrWhiteSpace(line) == false)
                {
                    // Convert the string to an int
                    int number = int.Parse(line);

                    // Add the value to the current total
                    currentTotal += number;
                }
                else
                {
                    // If the line does not have data, then
                    // prepare to move on to the next elf

                    // If the current total is greater than
                    // the highest total so far, set the
                    // highest total to the total of the elf
                    // that has just been processed
                    if (currentTotal > highestTotal)
                    {
                        highestTotal = currentTotal;
                    }

                    // Reset the current total to start counting
                    // for the next elf
                    currentTotal = 0;
                }
            }

            // Return the highest total as the answer
            return highestTotal.ToString();
        }
    }
}
