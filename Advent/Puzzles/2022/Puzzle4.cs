using Advent.IO;

namespace Advent.Puzzles._2022
{
    // Day 4 Puzzle
    // Creating the methods to split up the line into
    // their parts seemed like a logical thing to do
    // as it was likely they'd be needed for both
    // puzzles. I'd have preferred an if clause for
    // the overlap check during part 2, but I was
    // struggling to come up with how to do it that
    // wasn't a big if-or chain.
    internal class Puzzle4 : IPuzzle
    {
        public int Year { get; set; }

        // A separator between the start and end of a section
        private const char sectionSeparator = '-';

        // A separator between a pair
        private const char pairSeparator = ',';

        // Work out how many pairs completely overlap each other
        public string SolvePuzzle1(string file)
        {
            // Load in all the lines from the file
            List<string> lines = FileParser.ReadInputFileAsLines(file, Year);

            // Start the number of overlapping pairs at 0
            int overlappingPairs = 0;

            // Go through each line, which contains a pair
            foreach (string line in lines)
            {
                // Get the cleaning assignments for the pair, with the
                // first assignment stored in first, and the second in
                // second
                GetAssignments(line, out string first, out string second);

                // Get the start and end sections for the first assignment
                GetSectionStartAndEndValues(first, out int firstStart, out int firstEnd);

                // Get the start and end sections for the second assignment
                GetSectionStartAndEndValues(second, out int secondStart, out int secondEnd);

                // If the starting position of the first assignment is
                // the same or greater than the starting position of the
                // second assignment, and the end of the first assignement
                // is the same or less than the end position of the
                // second assignment then there is an overlap.
                // This also checks for the inverse where the second
                // assignment is inside the first
                if ((firstStart >= secondStart && firstEnd <= secondEnd)
                    || (secondStart >= firstStart && secondEnd <= firstEnd))
                {
                    // The pairs overlap, so increase the counter
                    overlappingPairs++;
                }
            }

            // Return the number of overlapping pairs as the answer
            return overlappingPairs.ToString();
        }

        // Work out how many pairs overlap at all
        public string SolvePuzzle2(string file)
        {
            // Load in all the lines from the file
            List<string> lines = FileParser.ReadInputFileAsLines(file, Year);

            // Start the number of overlapping pairs at 0
            int overlappingPairs = 0;

            // Go through each line, which contains a pair
            foreach (string line in lines)
            {
                // Get the cleaning assignments for the pair, with the
                // first assignment stored in first, and the second in
                // second
                GetAssignments(line, out string first, out string second);

                // Get the start and end sections for the first assignment
                GetSectionStartAndEndValues(first, out int firstStart, out int firstEnd);

                // Get the start and end sections for the second assignment
                GetSectionStartAndEndValues(second, out int secondStart, out int secondEnd);

                // Create a list which will contain all sections in
                // the first assignment
                List<int> firstAssignmentSections = new List<int>();

                // Loop from the start of the first section until the end
                for (int i = firstStart; i <= firstEnd; i++)
                {
                    // Add the value to the list
                    firstAssignmentSections.Add(i);
                }

                // Loop from the start of the second section until the end
                for (int i = secondStart; i <= secondEnd; i++)
                {
                    // If the list of assignments in the first section
                    // contains this value
                    if (firstAssignmentSections.Contains(i))
                    {
                        // The pairs overlap, so increase the counter
                        overlappingPairs++;

                        // Stop looping through the second assignment
                        break;
                    }
                }
            }

            // Return the number of overlapping pairs as the answer
            return overlappingPairs.ToString();
        }

        // Get the assignments from a pair
        private void GetAssignments(string pair, out string first, out string second)
        {
            // Get the index of the pair seperator (,)
            int separator = pair.IndexOf(pairSeparator);

            // Get the first section of a pair by returning
            // everything from the start until the separator
            first = pair.Substring(0, separator);

            // Get the second section of a pair by returning
            // everything after the separator
            // (index of separator + 1)
            second = pair.Substring(separator + 1);
        }

        // Get the start and end values in a section
        private void GetSectionStartAndEndValues(string section, out int start, out int end)
        {
            // Get the index of the section separator (-)
            int separator = section.IndexOf(sectionSeparator);

            // Get the first number in a section by converting
            // everything from the start until the separator into
            // an int (e.g. 4-8 will return 4)
            int.TryParse(section.Substring(0, separator), out start);

            // Get the second number in a section by converting
            // everything after the separator (index of seperator +1)
            // into an int (e.g. 4-8 will return 8)
            int.TryParse(section.Substring(separator + 1), out end);
        }
    }
}
