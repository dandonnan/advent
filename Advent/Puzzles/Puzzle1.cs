using Advent.IO;

namespace Advent.Puzzles
{
    internal class Puzzle1 : IPuzzle
    {
        public string SolvePuzzle1(string file)
        {
            int total = 0;

            var lines = FileParser.ReadInputFileAsLines(file);

            foreach (var line in lines)
            {
                total += GetPlainDigitsFromLine(line);
            }

            return total.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            int total = 0;

            var lines = FileParser.ReadInputFileAsLines(file);

            foreach (var line in lines)
            {
                total += GetAllDigitsFromLine(line);
            }

            return total.ToString();
        }

        // Get the digits from plain numeric values set in the line
        private static int GetPlainDigitsFromLine(string line)
        {
            int firstDigit = -1;
            int lastDigit = -1;

            for (int i=0; i<line.Length; i++)
            {
                // If the start of the string at the current index is an integer
                if (int.TryParse(line.AsSpan(i, 1), out int value))
                {
                    // Set it as the first value if not already set
                    if (firstDigit == -1)
                    {
                        firstDigit = value;
                    }
                    else
                    {
                        // Or set as the last value - this will keep getting set
                        // until the end of the string
                        lastDigit = value;
                    }
                }
            }

            // If no last digit has been set, use the first digit
            if (lastDigit == -1)
            {
                lastDigit = firstDigit;
            }

            // Combine the two digits by multiplying the first by 10
            // e.g. 2 and 1 becomes 20 + 1 = 21
            return (firstDigit * 10) + lastDigit;
        }

        // Get the digits from both numeric and string values set in the line
        private static int GetAllDigitsFromLine(string line)
        {
            int firstDigit = -1;
            int lastDigit = -1;

            // Get a list of keys that represent numeric values as strings
            // e.g. "one" = 1
            var keys = DigitStringMap.Select(map => map.Key).ToList();

            for (int i = 0; i < line.Length; i++)
            {
                // Try and get the numeric value as the above method
                if (int.TryParse(line.AsSpan(i, 1), out int value))
                {
                    if (firstDigit == -1)
                    {
                        firstDigit = value;
                    }
                    else
                    {
                        lastDigit = value;
                    }
                }
                else
                {
                    // If the value at the index is not numeric, go through
                    // the keys and see if the line starts with that key
                    foreach (var key in keys)
                    {
                        if (line.AsSpan(i, line.Length - i).StartsWith(key))
                        {
                            // If the line starts with the key, get the corresponding
                            // value (e.g. "one" = 1)
                            value = DigitStringMap[key];

                            // Set the first digit if not already set
                            if (firstDigit == -1)
                            {
                                firstDigit = value;
                            }
                            else
                            {
                                lastDigit = value;
                            }

                            break;
                        }
                    }
                }
            }

            if (lastDigit == -1)
            {
                lastDigit = firstDigit;
            }

            return (firstDigit * 10) + lastDigit;
        }

        private static readonly Dictionary<string, int> DigitStringMap = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };
    }
}
