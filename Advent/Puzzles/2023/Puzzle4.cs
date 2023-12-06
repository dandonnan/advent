using Advent.IO;

namespace Advent.Puzzles._2023
{
    internal class Puzzle4 : IPuzzle
    {
        public int Year { get; set; }

        public string SolvePuzzle1(string file)
        {
            int points = 0;

            var lines = FileParser.ReadInputFileAsLines(file, Year);

            foreach (var line in lines)
            {
                int scratchcardPoints = 0;

                // Get all winning numbers for the scratchcard
                var winningNumbers = GetWinningNumbers(line);

                // Get all numbers on the scratchcard
                var scratchcardNumbers = GetScratchcardNumbers(line);

                // Go through each number on the scratchcard
                foreach (int number in scratchcardNumbers)
                {
                    // If the number is a winning number
                    if (winningNumbers.Contains(number))
                    {
                        // If there are currently no points then start at 1
                        if (scratchcardPoints == 0)
                        {
                            scratchcardPoints = 1;
                        }
                        else
                        {
                            // Otherwise double the current score
                            scratchcardPoints *= 2;
                        }
                    }
                }

                // Add the score from the scratchcard to the total score
                points += scratchcardPoints;
            }

            return points.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            int totalCards = 0;

            var lines = FileParser.ReadInputFileAsLines(file, Year);

            Dictionary<int, Tuple<int, int>> scratchcardMatches = new Dictionary<int, Tuple<int, int>>();

            foreach (var line in lines)
            {
                // Get the winning numbers on the line
                var winningNumbers = GetWinningNumbers(line);

                // Get the numbers on the scratchcard
                var scratchcardNumbers = GetScratchcardNumbers(line);

                // Get the total matching numbers on the line
                var matchingNumbers = GetMatchingNumbers(winningNumbers, scratchcardNumbers);

                // Get the scratchcard's number
                var cardNumber = GetCardNumber(line);

                // Add the scratchcard number with how many matching numbers
                // it has to the dictionary
                scratchcardMatches.Add(cardNumber, new Tuple<int, int>(matchingNumbers, 1));
            }

            // Go through each scratchcard
            foreach (var scratchcard in scratchcardMatches)
            {
                // Add the scratchcard's number of instances to the total
                totalCards += scratchcard.Value.Item2;

                // Get the number of matching numbers on the scratchcard
                int matches = scratchcard.Value.Item1;

                if (matches > 0)
                {
                    // Go through each match
                    for (int i = 1; i <= matches; i++)
                    {
                        // As long as the subsequent card is within the total number of scratchcards
                        if (scratchcard.Key + i <= scratchcardMatches.Count)
                        {
                            // Get the existing totals for that scratchcard
                            var existing = scratchcardMatches[scratchcard.Key + i];

                            // Increase the number of instances on the subsequent card by
                            // however many instances of the current scratchcard exist
                            int cards = existing.Item2 + scratchcard.Value.Item2;

                            // Update the card
                            scratchcardMatches[scratchcard.Key + i] = new Tuple<int, int>(existing.Item1, cards);
                        }
                    }
                }
            }

            return totalCards.ToString();
        }

        private static int GetCardNumber(string line)
        {
            // Remove the card declaration from the line
            line = line.Replace("Card", string.Empty);

            // Set the stopping point to after the card declaration
            int stopIndex = line.IndexOf(":");

            // Get the card number
            line = line.Substring(0, stopIndex).Trim();

            return int.Parse(line);
        }

        private static List<int> GetWinningNumbers(string line)
        {
            List<int> winningNumbers = new List<int>();

            // Start after the card declaration
            int startIndex = line.IndexOf(":") + 1;

            // Stop before the list of numbers on the scratchcard
            int stopIndex = line.IndexOf("|");

            // Get the list of numbers between the start and end points
            var numbers = line.Substring(startIndex, stopIndex - startIndex).Trim().Split(" ");

            // Add each number to the list
            foreach (var number in numbers)
            {
                if (string.IsNullOrWhiteSpace(number) == false)
                {
                    winningNumbers.Add(int.Parse(number));
                }
            }

            return winningNumbers;
        }

        private static List<int> GetScratchcardNumbers(string line)
        {
            List<int> scratchcardNumbers = new List<int>();

            // Start after the scratchcard number declaration
            int startIndex = line.IndexOf("|") + 1;

            // Get the list of numbers after the start point
            var numbers = line.Substring(startIndex).Trim().Split(" ");

            // Add each number to the list
            foreach (var number in numbers)
            {
                if (string.IsNullOrWhiteSpace(number) == false)
                {
                    scratchcardNumbers.Add(int.Parse(number));
                }
            }

            return scratchcardNumbers;
        }

        private static int GetMatchingNumbers(List<int> winningNumbers, List<int> scratchcardNumbers)
        {
            int matching = 0;

            foreach (var number in scratchcardNumbers)
            {
                if (winningNumbers.Contains(number))
                {
                    matching += 1;
                }
            }

            return matching;
        }
    }
}
