using Advent.IO;

namespace Advent.Puzzles._2022
{
    // Day 6 Puzzle
    // The sample data was able to provide more test
    // cases, instead of each test being the sample
    // and the final solution. And I also got to use
    // the ReadInputFile method, which I was starting
    // to think would never be used.
    internal class Puzzle6 : IPuzzle
    {
        public int Year { get; set; }

        // Work out the index of the start of a message
        // determined by a chain of 4 unique characters 
        public string SolvePuzzle1(string file)
        {
            return SolvePuzzle(file, 4);
        }

        // Work out the index of the start of a message
        // determined by a chain of 14 unique characters 
        public string SolvePuzzle2(string file)
        {
            return SolvePuzzle(file, 14);
        }

        // Solve the puzzle using the given number of
        // unique characters
        public string SolvePuzzle(string file, int uniqueCharacters)
        {
            // Read in all the data from the file
            string dataStream = FileParser.ReadInputFile(file, Year);

            // Create an empty list of characters
            List<char> characters = new List<char>();

            // Start the index at 0
            int index = 0;

            // For each character in the data stream
            foreach (char character in dataStream)
            {
                // If the index is below the number of
                // unique characters
                if (index < uniqueCharacters)
                {
                    // Add the character to the list
                    characters.Add(character);

                    // Increase the index
                    index++;

                    // Skip over to the next character in the loop
                    continue;
                }

                // Increase the index to move to the next character
                index++;

                // Add the character to the characters list
                characters.Add(character);

                // Remove the first character in the list
                // The list should always contain the unique
                // number of characters, and since it kept
                // populating until it had enough at the start
                // of the loop, it now has too many so one
                // must be removed.
                characters.RemoveAt(0);

                // If the number of distinct characters in the list
                // matches the number of unique characters
                if (characters.Distinct().Count() == uniqueCharacters)
                {
                    // Stop the loop - the current index is the answer
                    break;
                }
            }

            // Return the index as the answer
            return index.ToString();
        }

        // The original solution to the first puzzle
        // before refactoring to reuse code in the second
        public string SolvePuzzle1_Original(string file)
        {
            // Read in all the data from the file
            string dataStream = FileParser.ReadInputFile(file, Year);

            // Determine the number of unique characters
            int uniqueCharacters = 4;

            // Create an empty list of characters
            List<char> characters = new List<char>();

            // Start the index at 0
            int index = 0;

            // For each character in the data stream
            foreach (char character in dataStream)
            {
                // If the index is below the number of
                // unique characters
                if (index < uniqueCharacters)
                {
                    // Add the character to the list
                    characters.Add(character);

                    // Increase the index
                    index++;

                    // Skip over to the next character in the loop
                    continue;
                }

                // If the character is not in the list of characters
                // and the number of distinct characters in the list
                // matches the number of unique characters
                if (characters.Contains(character) == false
                    && characters.Distinct().Count() == uniqueCharacters)
                {
                    // Stop the loop - the current index is the answer
                    break;
                }

                // Increase the index to move to the next character
                index++;

                // Add the character to the characters list
                characters.Add(character);

                // Remove the first character in the list
                // The list should always contain the unique
                // number of characters, and since it kept
                // populating until it had enough at the start
                // of the loop, it now has too many so one
                // must be removed.
                characters.RemoveAt(0);
            }

            // Return the index as the answer
            return index.ToString();
        }
    }
}
