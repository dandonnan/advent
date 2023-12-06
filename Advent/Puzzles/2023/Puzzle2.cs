using Advent.IO;

namespace Advent.Puzzles._2023
{
    internal class Puzzle2 : IPuzzle
    {
        public int Year { get; set; }

        public string SolvePuzzle1(string file)
        {
            int possibleGames = 0;

            var lines = FileParser.ReadInputFileAsLines(file, Year);

            foreach (var line in lines)
            {
                // Get the game id
                int gameId = ParseGameId(line);

                // Calculate if the game is possible
                if (IsGamePossible(12, 13, 14, line))
                {
                    // If possible, add the game id to the total
                    possibleGames += gameId;
                }
            }

            return possibleGames.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            int totalPower = 0;

            var lines = FileParser.ReadInputFileAsLines(file, Year);

            foreach (var line in lines)
            {
                totalPower += GetGamePower(line);
            }

            return totalPower.ToString();
        }

        private static int ParseGameId(string line)
        {
            // Remove "Game" from the line
            line = line.Replace("Game", string.Empty).Trim();

            // Parse the string that came after Game up to the first colon
            // to get the id
            return int.Parse(line.Substring(0, line.IndexOf(":")));
        }

        private static bool IsGamePossible(int redCubes, int greenCubes, int blueCubes, string game)
        {
            bool possible = true;

            // Get the sets in the game
            var sets = ParseSets(game);

            // Go through each set
            foreach (var set in sets)
            {
                // If there are more cubes in the set than should be possible then flag
                // the game as impossible
                if (set.Item1 > redCubes || set.Item2 > greenCubes || set.Item3 > blueCubes)
                {
                    possible = false;
                    break;
                }
            }

            return possible;
        }

        private static int GetGamePower(string game)
        {
            int red = 0;
            int green = 0;
            int blue = 0;

            // Get the sets in the game
            var sets = ParseSets(game);

            // Go through each set and set the highest of each value
            foreach (var set in sets)
            {
                if (set.Item1 > red)
                {
                    red = set.Item1;
                }

                if (set.Item2 > green)
                {
                    green = set.Item2;
                }

                if (set.Item3 > blue)
                {
                    blue = set.Item3;
                }
            }

            // Multiply the highest of each value to get the power
            return red * green * blue;
        }

        // Parse the sets for a game, where a set is <Red, Green, Blue>
        private static List<Tuple<int, int, int>> ParseSets(string game)
        {
            List<Tuple<int, int, int>> sets = new List<Tuple<int, int, int>>();

            // Sets don't begin until after the colon, some remove the start
            // of the string
            game = game.Substring(game.IndexOf(":") + 1);

            // Sets are split by semicolons, so divide them up
            string[] split = game.Split(";");

            // Go through each set
            foreach (string set in split)
            {
                int red = 0;
                int green = 0;
                int blue = 0;

                // Colours are separated by a comma so split them up
                string[] colours = set.Split(",");

                foreach (string colour in colours)
                {
                    // Add the totals for each colour
                    red += ParseColour("red", colour);
                    green += ParseColour("green", colour);
                    blue += ParseColour("blue", colour);
                }

                sets.Add(new Tuple<int, int, int>(red, green, blue));
            }

            return sets;
        }

        private static int ParseColour(string colourName, string set)
        {
            int amount = 0;

            // Get the index of the colour in the string
            int index = set.IndexOf(colourName);

            // If the colour is listed in the string
            if (index > -1)
            {
                // Get the total of that colour by parsing the string before
                // the colour
                amount = int.Parse(set.Substring(0, index).Trim());
            }

            return amount;
        }
    }
}
