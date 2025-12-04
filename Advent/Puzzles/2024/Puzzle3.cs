using Advent.IO;

namespace Advent.Puzzles._2024
{
    internal class Puzzle3 : IPuzzle
    {
        public int Year { get; set; }

        public string SolvePuzzle1(string file)
        {
            // Read in the input as a continuous string
            var input = FileParser.ReadInputFile(file, Year);

            // Get all valid multipliers
            var multipliers = GetValidMultipliers(input);

            // Get the total of all valid multiplied values
            var total = multipliers.Sum(m => m.Item1);

            return total.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            throw new NotImplementedException();
        }

        // Gets a tuple of all valid multipliers where the first number
        // is the multiplied amount of the second and third numbers
        // I tried to anticipate what part 2 would involve and suspected
        // it would ask me to do something different with the two values
        // which is why I keep them, but I was wildly out
        private List<Tuple<int, int, int>> GetValidMultipliers(string input)
        {
            var validList = new List<Tuple<int, int, int>>();

            // The identifier for a multiplier instruction
            var mulIdentifier = "mul(";

            // The first index of where the multiplier occurs in the input
            var mulIndex = input.IndexOf(mulIdentifier);

            // Loop as long as the identifier exists within the input
            while (mulIndex > -1)
            {
                // If the index is outside the input's length then end
                // the loop
                if (mulIndex + mulIdentifier.Length > input.Length)
                {
                    mulIndex = -1;
                    continue;
                }

                // Trim the input to start after the first identifier
                input = input.Substring(mulIndex + mulIdentifier.Length);

                // Get the index of the next identifier for the next time the
                // loop cycles
                mulIndex = input.IndexOf(mulIdentifier);

                // Get the first index of the comma separator
                var commaIndex = input.IndexOf(",");

                // Get the first index of the close bracket separator
                var bracketIndex = input.IndexOf(")");

                // If either the comma or bracket is in a position that
                // is not allowed, then skip to the next cycle
                if (commaIndex > bracketIndex || commaIndex <= 0
                    || commaIndex > 3 || bracketIndex > 7
                    || bracketIndex < 3)
                {
                    continue;
                }

                // Attempt to get the first number by parsing between the start
                // of the input and the first comma
                if (!int.TryParse(input.Substring(0, commaIndex), out var number1))
                {
                    // If it is not a number then skip
                    continue;
                }

                // Attempt to get the second number by parsing after the first comma
                // and before the first bracket
                if (!int.TryParse(input.Substring(commaIndex + 1, bracketIndex - commaIndex - 1), out var number2))
                {
                    // If it is not a number then skip
                    continue;
                }

                // Add the numbers to the list, including the multiplied amount
                validList.Add(new Tuple<int, int, int>(number1 * number2, number1, number2));

                // Trim the input to be after the bracket
                input = input.Substring(bracketIndex + 1);

                // Update the index to check the trimmed input
                mulIndex = input.IndexOf(mulIdentifier);
            }

            return validList;
        }
    }
}
