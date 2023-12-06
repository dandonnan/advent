using Advent.IO;

namespace Advent.Puzzles._2023
{
    internal class Puzzle3 : IPuzzle
    {
        public int Year { get; set; }

        public string SolvePuzzle1(string file)
        {
            int total = 0;

            var lines = FileParser.ReadInputFileAsLines(file, Year);

            List<Schematic> schematics = new List<Schematic>();

            for (int i=0; i<lines.Count; i++)
            {
                // Get all schematic information from the lines
                schematics.AddRange(GetSchematics(lines[i], i));
            }

            // Get all numeric parts
            var numerics = schematics.Where(s => s.Numeric);

            // Go through each numeric part
            foreach (var schematic in numerics)
            {
                // Set the line range to check to be above and below the
                // schematic's line
                int minLine = schematic.LineNumber - 1;
                int maxLine = schematic.LineNumber + 1;

                // Set the index to check to be 1 before the current index
                // and the current index plus the length of the schematic's value
                int minIndex = schematic.Index - 1;
                int maxIndex = schematic.Index + schematic.Length;

                // Find all neighbouring schematics within the line range
                // and index range that aren't numeric (symbols)
                // xOOOOOOOx    If value is the schematic's value
                // xOvalueOx    then O indicates all places to be
                // xOOOOOOOx    checked for symbols
                var symbols = schematics.Where(s => s.LineNumber >= minLine && s.LineNumber <= maxLine
                                                    && s.Index >= minIndex && s.Index <= maxIndex
                                                    && s.Numeric == false);

                // If there are neighbouring symbols then add the
                // value to the total
                if (symbols.Any())
                {
                    total += int.Parse(schematic.Value);
                }
            }

            return total.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            int total = 0;

            var lines = FileParser.ReadInputFileAsLines(file, Year);

            List<Schematic> schematics = new List<Schematic>();

            for (int i = 0; i < lines.Count; i++)
            {
                // Get all schematic information from the lines
                schematics.AddRange(GetSchematics(lines[i], i));
            }

            // Get all gears (indicated by *)
            var gears = schematics.Where(s => s.Value == "*");

            // Go through each gear
            foreach (var schematic in gears)
            {
                // Set the line range to check to be above and below the
                // schematic's line
                int minLine = schematic.LineNumber - 1;
                int maxLine = schematic.LineNumber + 1;

                // Find all neighbouring schematics within the line range
                // and index range that are numeric
                var symbols = schematics.Where(s => s.LineNumber >= minLine && s.LineNumber <= maxLine
                                                    && s.Index >= schematic.Index - s.Length && s.Index <= schematic.Index + 1
                                                    && s.Numeric);

                // If there are neighbouring symbols then add the
                // value to the total
                if (symbols.Count() == 2)
                {
                    total += int.Parse(symbols.First().Value) * int.Parse(symbols.Last().Value);
                }
            }

            return total.ToString();
        }

        private static List<Schematic> GetSchematics(string line, int lineIndex)
        {
            List<Schematic> schematics = new List<Schematic>();

            // Get all the individual parts of the line
            var split = SplitLineIntoParts(line);

            int index = 0;

            // Go through each part
            foreach (var part in split)
            {
                // If the part is not empty
                if (string.IsNullOrWhiteSpace(part) == false)
                {
                    // Create a schematic with the current line
                    // and index position for the part
                    schematics.Add(new Schematic
                    {
                        LineNumber = lineIndex,
                        Index = index,
                        Value = part
                    });

                    // Move the index to after the part
                    index += part.Length;
                }
                else
                {
                    // Move the index along
                    index++;
                }
            }

            return schematics;
        }

        private static List<string> SplitLineIntoParts(string line)
        {
            List<string> split = new List<string>();

            // As long as the line has content
            while (line.Length > 0)
            {
                // If the line starts with a . then add an empty
                // string to the list and move the line to the next
                // character
                if (line.Substring(0, 1) == ".")
                {
                    split.Add(string.Empty);

                    line = line.Substring(1);
                }
                else
                {
                    int numberOffset = 1;

                    // If the line starts with a number
                    if (int.TryParse(line.Substring(0, numberOffset), out int value))
                    {
                        // Keep moving along the line until the first non numeric index
                        while (int.TryParse(line.Substring(0, numberOffset), out value))
                        {
                            numberOffset++;

                            // If the end of the line is reached then stop
                            if (numberOffset > line.Length)
                            {
                                break;
                            }
                        }

                        // Go back one place to where the start of the line was still
                        // numeric
                        numberOffset -= 1;

                        // Add the number to the list
                        split.Add(line.Substring(0, numberOffset));

                        // Remove the number from the line
                        line = line.Substring(numberOffset);
                    }
                    else
                    {
                        // If the line does not start with a number, add the
                        // symbol to the list
                        split.Add(line.Substring(0, 1));

                        // Move to the next part of the line
                        line = line.Substring(1);
                    }
                }
            }

            return split;
        }

        private struct Schematic
        {
            // The line number in the file that the part appears
            public int LineNumber { get; set; }

            // The index in the line that the part appears
            public int Index { get; set; }

            // The part's value
            public string Value { get; set; }

            // Whether the part is numeric
            public bool Numeric => int.TryParse(Value, out _);

            // The length of the part
            public int Length => Value.Length;
        }
    }
}
