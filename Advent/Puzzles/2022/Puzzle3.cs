using Advent.IO;

namespace Advent.Puzzles._2022
{
    // Day 3 Puzzle
    // I wanted to try and combine the get priorities
    // methods, but with puzzle 1 splitting each line
    // into 2 and puzzle 2 grouping them in threes,
    // it was probably better to keep them separate 
    // instead of muddying the logic. The final
    // foreach in each of those methods could be
    // pulled out into a seperate method though.
    internal class Puzzle3 : IPuzzle
    {
        public int Year { get; set; }

        // All the characters in the alphabet
        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        // Work out the total priority of the items
        // that appear in both rucksacks
        public string SolvePuzzle1(string file)
        {
            // Get the total amount
            int priority = GetTotalPriorities(file);

            // Return the total priority as the answer
            return priority.ToString();
        }

        // Work out the total priority of the authenticity
        // badges that appear in the rucksacks
        public string SolvePuzzle2(string file)
        {
            // Get the total amount
            int priority = GetBadgePriority(file, 3);

            // Return the total priority as the answer
            return priority.ToString();
        }

        // Get the total priorities from the file
        private int GetTotalPriorities(string file)
        {
            // Start the total as 0
            int totalPriority = 0;

            // Load in each line from the file
            List<string> lines = FileParser.ReadInputFileAsLines(file, Year);

            // Generate a dictionary of priority values
            Dictionary<char, int> priorities = GenerateLetterPriorities();

            // Go through each line
            foreach (string line in lines)
            {
                // The first half of the line is the first rucksack
                string rucksack1 = line.Substring(0, line.Length / 2);

                // The second half of the line is the second rucksack
                string rucksack2 = line.Substring(rucksack1.Length);

                // Go through each character in the first rucksack
                foreach (char character in rucksack1)
                {
                    // If the second rucksack contains that character
                    if (rucksack2.Contains(character))
                    {
                        // Get the priority value for that character
                        priorities.TryGetValue(character, out int value);

                        // Add the value to the total priority
                        totalPriority += value;

                        // Stop looping through the first rucksack
                        break;
                    }
                }
            }

            // Return the total priority
            return totalPriority;
        }

        // Get the priority of all authenticity badges from
        // the file
        private int GetBadgePriority(string file, int rucksackGroups)
        {
            // Start the total at 0
            int totalPriority = 0;

            // Read in all the lines from the file
            List<string> lines = FileParser.ReadInputFileAsLines(file, Year);

            // Generate a dictionary of priority values
            Dictionary<char, int> priorities = GenerateLetterPriorities();

            // Go through each line, but one rucksack group at a time
            for (int i = 0; i < lines.Count; i += rucksackGroups)
            {
                // Create a list of repeated characters
                List<char> repeatedCharacters = new List<char>();

                // Get the first line in the group
                string line1 = lines[i];

                // Get the second line in the group
                string line2 = lines[i + 1];

                // Get the third line in the group
                string line3 = lines[i + 2];

                // Go through each character in line 1
                foreach (char character in line1)
                {
                    // If line 2 contains that character
                    if (line2.Contains(character))
                    {
                        // Add it to the repeated characters list
                        repeatedCharacters.Add(character);

                        // Note: this doesn't stop here because there
                        // may be another repeated character which
                        // is the one that overlaps with line 3
                    }
                }

                // Go through each of the repeated characters
                foreach (char character in repeatedCharacters)
                {
                    // If line 3 contains a repeated character
                    if (line3.Contains(character))
                    {
                        // Get the priority value for that character
                        priorities.TryGetValue(character, out int value);

                        // Add the value to the total
                        totalPriority += value;

                        // Stop looping through the repeated
                        // characters
                        break;
                    }
                }
            }

            // Return the total priority
            return totalPriority;
        }

        // Systematically generate a dictionary of letters and
        // their priority values
        private Dictionary<char, int> GenerateLetterPriorities()
        {
            // Create an empty dictionary
            Dictionary<char, int> priorities = new Dictionary<char, int>();

            // Get an upper case version of the alphabet
            string upperCase = alphabet.ToUpper();

            // Get the length of the alphabet
            int length = alphabet.Length;

            // Go through the alphabet
            for (int i = 0; i < length; i++)
            {
                // Add a lower case value at the current
                // index and set the priority to be the
                // index + 1 (a is at index 0, but has
                // a priority of 1).
                priorities.Add(alphabet[i], i + 1);

                // Add an upper case value at the current
                // index and set the priority to be the
                // index + 1 + the length of the alphabet
                // (a is at index 0, but A should have
                // a priority of 27 - 0+26+1=27).
                priorities.Add(upperCase[i], i + length + 1);
            }

            // Return the dictionary
            return priorities;
        }
    }
}
