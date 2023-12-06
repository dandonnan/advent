using Advent.IO;

namespace Advent.Puzzles._2022
{
    // Day 2 Puzzle
    // I wanted a shared ParseHand method where
    // I could pass in a dictionary and points
    // that could be awarded, but couldn't think
    // of decent names for the parameters (they
    // are not as simple as win, lose, draw).
    internal class Puzzle2 : IPuzzle
    {
        public int Year { get; set; }

        // The amount of points for winning
        private const int pointsPerWin = 6;

        // The amount of points for a draw
        private const int pointsPerDraw = 3;

        // The amount of points for losing
        private const int pointsPerLoss = 0;

        // The amount of points playing Rock earns
        private const int pointsRock = 1;

        // The amount of points playing Paper earns
        private const int pointsPaper = 2;

        // The amount of points playing Scissors earns
        private const int pointsScissors = 3;

        // Work out how many points will be earned playing
        // Rock, Paper, Scissors against the elves
        public string SolvePuzzle1(string file)
        {
            // Get a list of hands from the file
            List<string> hands = FileParser.ReadInputFileAsLines(file, Year);

            // Create a tracker for the number of points
            int points = 0;

            // Go through each hand
            foreach (string hand in hands)
            {
                // Parse the hand and add the result to the total
                points += ParseHandForPuzzle1(hand);
            }

            // Return the total points as the answer
            return points.ToString();
        }

        // Work out how many points will be earned when
        // deliberately winning / drawing / losing
        public string SolvePuzzle2(string file)
        {
            // Get a list of hands from the file
            List<string> hands = FileParser.ReadInputFileAsLines(file, Year);

            // Create a tracker for the number of points
            int points = 0;

            // Go through each hand
            foreach (string hand in hands)
            {
                // Parse the hand and add the result to the total
                points += ParseHandForPuzzle2(hand);
            }

            // Return the total points as the answer
            return points.ToString();
        }

        // Parse the hand for the first puzzle, where XYZ
        // represent either Rock, Paper or Scissors
        private int ParseHandForPuzzle1(string hand)
        {
            // Get the move played by the elf (ABC)
            char elf = hand.First();

            // Get the move played by the player (XYZ)
            char player = hand.Last();

            // Lookup the player's move in the Points
            // dictionary to get how many points it is
            // worth
            PointsPerMove.TryGetValue(player, out int points);

            // Determine additional behavior based on
            // what the elf's move was
            switch (elf)
            {
                // If the elf played A (Rock), increment
                // the number of points accordingly
                // X (Rock) = Draw
                // Y (Paper) = Win
                // Z (Scissors) = Loss
                case 'A':
                    points += player == 'X' ? pointsPerDraw
                            : player == 'Y' ? pointsPerWin
                            : pointsPerLoss;
                    break;

                // If the elf played B (Paper), increment
                // the number of points accordingly
                // X (Rock) = Loss
                // Y (Paper) = Draw
                // Z (Scissors) = Win
                case 'B':
                    points += player == 'Y' ? pointsPerDraw
                            : player == 'Z' ? pointsPerWin
                            : pointsPerLoss;
                    break;

                // If the elf played C (Scissors), increment
                // the number of points accordingly
                // X (Rock) = Win
                // Y (Paper) = Loss
                // Z (Scissors) = Draw
                case 'C':
                    points += player == 'Z' ? pointsPerDraw
                            : player == 'X' ? pointsPerWin
                            : pointsPerLoss;
                    break;
            }

            return points;
        }

        // Parse the hand for the second puzzle, where XYZ
        // represent whether to win, lose or draw
        private int ParseHandForPuzzle2(string hand)
        {
            // Get the move played by the elf (ABC)
            char elf = hand.First();

            // Get the outcome for the player (XYZ)
            char player = hand.Last();

            // Lookup the player's outcome in the points
            // dictionary to get how many points it is
            // worth
            PointsPerOutcome.TryGetValue(player, out int points);

            // Determine additional behavior based on
            // what the elf's move was
            switch (elf)
            {
                // If the elf played A (Rock), increment
                // the number of points accordingly
                // X (Loss) = Scissors
                // Y (Draw) = Rock
                // Z (Win) = Paper
                case 'A':
                    points += player == 'X' ? pointsScissors
                            : player == 'Y' ? pointsRock
                            : pointsPaper;
                    break;

                // If the elf played B (Paper), increment
                // the number of points accordingly
                // X (Loss) = Rock
                // Y (Draw) = Paper
                // Z (Win) = Scissors
                case 'B':
                    points += player == 'X' ? pointsRock
                            : player == 'Y' ? pointsPaper
                            : pointsScissors;
                    break;

                // If the elf played C (Scissors), increment
                // the number of points accordingly
                // X (Loss) = Paper
                // Y (Draw) = Scissors
                // Z (Win) = Rock
                case 'C':
                    points += player == 'X' ? pointsPaper
                            : player == 'Y' ? pointsScissors
                            : pointsRock;
                    break;
            }

            return points;
        }

        // A dictionary containing the move chosen by
        // the player and the points they are worth
        private static Dictionary<char, int> PointsPerMove =
            new Dictionary<char, int>
        {
            // X (Rock)
            { 'X', pointsRock },

            // Y (Paper)
            { 'Y', pointsPaper },

            // Z (Scissors)
            { 'Z', pointsScissors }
        };

        // A dictionary containing the outcome of
        // a game and the points that should be awarded
        private static Dictionary<char, int> PointsPerOutcome =
            new Dictionary<char, int>
        {
            // X (Loss)
            { 'X', pointsPerLoss },

            // Y (Draw)
            { 'Y', pointsPerDraw },

            // Z (Win)
            { 'Z', pointsPerWin }
        };
    }
}
