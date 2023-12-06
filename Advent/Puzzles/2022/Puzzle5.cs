using Advent.IO;
using System.Text;

namespace Advent.Puzzles._2022
{
    // Day 5 Puzzle
    // While initially this looked like a harder challenge
    // because the data was in multiple formats, once the
    // data is parsed in it is pretty easy to manage.
    internal class Puzzle5 : IPuzzle
    {
        public int Year { get; set; }

        // The separator between each stack of crates
        private const string stackSeparator = " ";

        // The format of a crate
        private const string crateFormat = "[ ]";

        // An empty crate slot
        private const string emptySlot = "   ";

        // The move command
        private const string commandMove = "move";

        // The from command
        private const string commandFrom = "from";

        // The to command
        private const string commandTo = "to";

        // Move the crates between each stack, one at a
        // time, then work out what is at the top of
        // each stack
        public string SolvePuzzle1(string file)
        {
            // Call the solve puzzle function and use
            // the move one at a time command to move crates
            return SolvePuzzle(file, MoveCratesOneAtATime);
        }

        // Move the crates between each stack, multiple
        // at a time, then work out what is at the top
        // of each stack
        public string SolvePuzzle2(string file)
        {
            // Call the solve puzzle function and use
            // the move multiple command to move crates
            return SolvePuzzle(file, MoveMultipleCratesAtOnce);
        }

        // Solve the puzzle using the function passed in
        // to handle the moving of the crates
        private string SolvePuzzle(
            string file,
            Action<List<List<string>>, int, int, int> moveCrates)
        {
            // Load in the lines from the file
            List<string> lines = FileParser.ReadInputFileAsLines(file, Year);

            // Populate a list which contains lists of stacks
            List<List<string>> stacks = PopulateStacks(lines);

            // Get a list of all commands
            List<string> commands = GetCommands(lines);

            // Go through each command
            foreach (string command in commands)
            {
                // Parse the command to get the values that
                // determine where to move the crates from
                // and to, and how many to move
                ParseCommand(command, out int amount, out int from, out int to);

                // Call the function that was passed in and give it the
                // required parameters
                moveCrates(stacks, amount, from, to);
            }

            // Create a string builder
            StringBuilder stringBuilder = new StringBuilder();

            // Go through each stack
            foreach (List<string> stack in stacks)
            {
                // Add the crate contents from the first crate
                // in the stack to the string builder
                stringBuilder.Append(ParseCrateContents(stack.First()));
            }

            // Return the contents of the string builder, which
            // is the first crate in each stack, as the answer
            return stringBuilder.ToString();
        }

        // Move the crates one at a time
        private void MoveCratesOneAtATime(
            List<List<string>> stacks,
            int amount,
            int from,
            int to
        )
        {
            // Loop from 0 to the amount of crates to move
            for (int i = 0; i < amount; i++)
            {
                // Grab the first crate in the stack where the
                // crate is to be moved from
                string topStack = stacks[from].First();

                // Insert that crate at the top (0) of the
                // stack it should be moved to
                stacks[to].Insert(0, topStack);

                // Remove the crate at the top (0) of the
                // stack it was moved from
                stacks[from].RemoveAt(0);
            }
        }

        // Move multiple crates at once
        private void MoveMultipleCratesAtOnce(
            List<List<string>> stacks,
            int amount,
            int from,
            int to
        )
        {
            // Get an amount of crates from the top of the
            // stack to move them from
            IEnumerable<string> cratesToMove = stacks[from].Take(amount);

            // Insert the crates at the top (0) of the stack
            // they should be moved to
            stacks[to].InsertRange(0, cratesToMove);

            // Remove the crates at the top (0) of the stack
            // they were moved from
            stacks[from].RemoveRange(0, cratesToMove.Count());
        }

        // Populate the stacks
        private List<List<string>> PopulateStacks(List<string> lines)
        {
            // Create an empty list of lists
            List<List<string>> stacks = new List<List<string>>();

            // Track whether the first line is being parsed
            bool firstLine = true;

            // Go through each line
            foreach (string line in lines)
            {
                // If the line does not start with a crate ([ ])
                // and it is not an empty slot
                if (line.StartsWith(crateFormat[0]) == false
                    && line.StartsWith(emptySlot) == false)
                {
                    // Stop parsing lines
                    break;
                }

                // Track the current index of the line to
                // use as a start position
                int lineIndex = 0;

                // Track the current stack to add crates to
                int stackIndex = 0;

                // As long as the line index is not longer
                // than the length of the line
                while (lineIndex < line.Length)
                {
                    // If this is the first line
                    if (firstLine)
                    {
                        // Add a new empty list to the list of stacks
                        stacks.Add(new List<string>());
                    }

                    // If the character of the line at the line index
                    // matches the start of a crate
                    if (line[lineIndex] == crateFormat[0])
                    {
                        // Add the contents of the line from the line
                        // index, up to the length of the crate format
                        // (so 3 characters as the format is [ ]),
                        // to the stack at the stack index
                        stacks[stackIndex].Add(line.Substring(lineIndex, crateFormat.Length));
                    }

                    // Move the line index up by the length of a crate
                    // plus the length of a separator
                    lineIndex += crateFormat.Length + stackSeparator.Length;

                    // Move on to the next stack
                    stackIndex++;
                }

                // At least one line has been parsed, so this
                // is no longer the first
                firstLine = false;
            }

            // Return a list of stacks
            return stacks;
        }

        // Get a list of commands
        private List<string> GetCommands(List<string> lines)
        {
            // Create an empty list
            List<string> commands = new List<string>();

            // Go through each line
            foreach (string line in lines)
            {
                // If the line starts with the move command
                if (line.StartsWith(commandMove))
                {
                    // Add that command to the list
                    commands.Add(line);
                }
            }

            // Return the list of commands
            return commands;
        }

        // Parse a command
        private void ParseCommand(string command, out int amount, out int from, out int to)
        {
            // Remove all of the commands and replace them
            // with empty strings, so the string is just
            // numbers and spaces. Trim at the start to
            // remove the first space, and replace double
            // spaces with a single space.
            command = command.Replace(commandMove, string.Empty)
                             .Replace(commandFrom, string.Empty)
                             .Replace(commandTo, string.Empty)
                             .TrimStart()
                             .Replace("  ", " ");

            // Split the command into values by separating
            // the string whenever there is a space
            string[] values = command.Split(' ');

            // The amount value will be the first the array
            amount = int.Parse(values[0]);

            // The from value will be the second in the array
            // 1 is removed because the command indexing starts
            // at 1 whereas code starts at 0
            from = int.Parse(values[1]) - 1;

            // The to value will be the second in the array
            // 1 is removed from the same reason as above
            to = int.Parse(values[2]) - 1;
        }

        // Parse the contents of a crate
        private string ParseCrateContents(string crate)
        {
            // Replace the [ and ] characters with blank
            // space, returning just the letter in the crate
            return crate.Replace("[", string.Empty)
                        .Replace("]", string.Empty);
        }

        public string SolvePuzzle1_Original(string file)
        {
            // Load in the lines from the file
            List<string> lines = FileParser.ReadInputFileAsLines(file, Year);

            // Populate a list which contains lists of stacks
            List<List<string>> stacks = PopulateStacks(lines);

            // Get a list of all commands
            List<string> commands = GetCommands(lines);

            // Go through each command
            foreach (string command in commands)
            {
                // Parse the command to get the values that
                // determine where to move the crates from
                // and to, and how many to move
                ParseCommand(command, out int amount, out int from, out int to);

                // Loop from 0 to the amount of crates to move
                for (int i = 0; i < amount; i++)
                {
                    // Grab the first crate in the stack where the
                    // crate is to be moved from
                    string topStack = stacks[from].First();

                    // Insert that crate at the top (0) of the
                    // stack it should be moved to
                    stacks[to].Insert(0, topStack);

                    // Remove the crate at the top (0) of the
                    // stack it was moved from
                    stacks[from].RemoveAt(0);
                }
            }

            // Create a string builder
            StringBuilder stringBuilder = new StringBuilder();

            // Go through each stack
            foreach (List<string> stack in stacks)
            {
                // Add the crate contents from the first crate
                // in the stack to the string builder
                stringBuilder.Append(ParseCrateContents(stack.First()));
            }

            // Return the contents of the string builder, which
            // is the first crate in each stack, as the answer
            return stringBuilder.ToString();
        }
    }
}
