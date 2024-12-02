using Advent.IO;

namespace Advent.Puzzles._2024
{
    internal class Puzzle2 : IPuzzle
    {
        public int Year { get; set; }

        public string SolvePuzzle1(string file)
        {
            var lines = FileParser.ReadInputFileAsLines(file, Year);

            // The number of lines that are safe
            var safeLines = 0;

            // Go through each line
            foreach (var line in lines)
            {
                // If the line is safe
                if (IsLineSafe(line))
                {
                    // Increase the number of safe lines
                    safeLines++;
                }
            }

            return safeLines.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            var lines = FileParser.ReadInputFileAsLines(file, Year);

            // The number of lines that are safe
            var safeLines = 0;

            // Go through each line
            foreach (var line in lines)
            {
                // If the line is safe
                if (IsLineSafe(line))
                {
                    // Increase the number of lines
                    safeLines++;
                }
                else
                {
                    // If the line isn't safe, split it
                    var split = line.Split(' ');

                    // For each number in the line
                    for (int i=0; i<split.Length; i++)
                    {
                        // Turn the split array into a list
                        var numbers = split.ToList();

                        // Remove the current number from the list
                        numbers.RemoveAt(i);

                        // Create a new line of the numbers, with the current
                        // number removed
                        var newLine = string.Join(" ", numbers);

                        // Check if the new line is safe
                        if (IsLineSafe(newLine))
                        {
                            safeLines++;
                            break;
                        }
                    }
                }
            }

            return safeLines.ToString();
        }

        
        // Check if a line in safe according to the rules:
        //  - Must be gradually increasing or decreasing
        //  - Can only change by a minimum of 1 and maximum of 3
        private bool IsLineSafe(string line)
        {
            // Split the line into "levels"
            var split = line.Split(' ');

            // Keep track of which direction the line is going in
            var direction = 0;

            // Go through each "level"
            for (var i=0; i<split.Length-1; i++)
            {
                // Get the current and next numbers
                var currentNumber = int.Parse(split[i]);
                var nextNumber = int.Parse(split[i+1]);

                // If no direction has been set
                if (direction == 0)
                {
                    // Set the direction to decrease
                    if (nextNumber < currentNumber)
                    {
                        direction = -1;
                    }

                    // Set the direction to increase
                    if (nextNumber > currentNumber)
                    {
                        direction = 1;
                    }

                    // The number hasn't changed, so it is unsafe
                    if (direction == 0)
                    {
                        return false;
                    }
                }

                // Create a list of valid numbers
                var validNumbers = new List<int>();

                for (var j=1; j<=3; j++)
                {
                    // The valid numbers are within a range of 1-3 in whichever
                    // direction the "levels" started to change
                    validNumbers.Add(currentNumber + (j * direction));
                }

                // If the next number is not valid then the line is not safe
                if (!validNumbers.Contains(nextNumber))
                {
                    return false;
                }
            }

            // The line is safe
            return true;
        }
    }
}
