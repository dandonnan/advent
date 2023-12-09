using Advent.IO;

namespace Advent.Puzzles._2023
{
    internal class Puzzle6 : IPuzzle
    {
        public int Year { get; set; }

        public string SolvePuzzle1(string file)
        {
            var lines = FileParser.ReadInputFileAsLines(file, Year);

            // Convert the lines into race records
            var records = GetRaceRecords(lines);

            int totalWins = 1;

            foreach (var record in records)
            {
                // Get the number of ways to win for the record
                int ways = GetWaysToWin(record);

                // Multiply it with the total number of wins
                totalWins *= ways;
            }

            return totalWins.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            var lines = FileParser.ReadInputFileAsLines(file, Year);

            // Manipulate both lines to get a single value for time and distance
            var time = lines.First().Substring(lines.First().IndexOf(":") + 1).Replace(" ", string.Empty);

            var distance = lines.Last().Substring(lines.Last().IndexOf(":") + 1).Replace(" ", string.Empty);

            // Create a record from the lines
            var record = new RaceRecord
            {
                Time = long.Parse(time),
                Distance = long.Parse(distance)
            };

            return GetWaysToWin(record).ToString();
        }

        private static List<RaceRecord> GetRaceRecords(List<string> lines)
        {
            List<RaceRecord> records = new List<RaceRecord>();

            // Get the values from each line
            var times = lines.First().Substring(lines.First().IndexOf(":") + 1).Trim();

            var distances = lines.Last().Substring(lines.Last().IndexOf(":") + 1).Trim();

            // Until all times have been parsed
            while (times.Length > 0)
            {
                // Get the index of the next separator
                var timeEnd = times.IndexOf(" ");

                var distanceEnd = distances.IndexOf(" ");

                // Get the time and distance by getting a substring from the start until the separator
                var time = timeEnd > -1 ? times.Substring(0, timeEnd) : times;

                var distance = distanceEnd > -1 ? distances.Substring(0, distanceEnd) : distances;

                // Remove the time and distance from the rest of the values
                times = times.Substring(time.Length).Trim();

                distances = distances.Substring(distance.Length).Trim();

                // Create a record
                records.Add(new RaceRecord
                {
                    Time = long.Parse(time),
                    Distance = long.Parse(distance)
                });
            }

            return records;
        }

        private static int GetWaysToWin(RaceRecord record)
        {
            long movingDistance = 0;

            int waysToWin = 0;

            for (long i=0; i<record.Time; i++)
            {
                // Get the distance travelled by multiplying the moving distance
                // with the time left to travel
                long distance = movingDistance * (record.Time - i);

                // Increase the moving distance with each second
                movingDistance++;

                // If the distance travelled beats the record then count
                // as a way to win
                if (distance > record.Distance)
                {
                    waysToWin++;
                }
            }

            return waysToWin;
        }

        private struct RaceRecord
        {
            public long Time { get; set; }

            public long Distance { get; set; }
        }
    }
}
