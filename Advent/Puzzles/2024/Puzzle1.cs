using Advent.IO;

namespace Advent.Puzzles._2024
{
    internal class Puzzle1 : IPuzzle
    {
        public int Year { get; set; }

        public string SolvePuzzle1(string file)
        {
            var lines = FileParser.ReadInputFileAsLines(file, Year);

            // Separate each line into lists of values on the left and
            // values on the right, then put them in ascending order
            List<int> leftList = GetLeftList(lines).OrderBy(n => n).ToList();
            List<int> rightList = GetRightList(lines).OrderBy(n => n).ToList();

            var total = 0;

            // For every item in the left list
            for (int i=0; i<leftList.Count; i++)
            {
                // Calculate the difference between the matching item
                // in the right list
                var distance = rightList[i] - leftList[i];

                // Add the difference to the total
                total += Math.Abs(distance);
            }

            return total.ToString();
        }

        public string SolvePuzzle2(string file)
        {
            var lines = FileParser.ReadInputFileAsLines(file, Year);

            // Separate each line into lists of values on the left and
            // values on the right
            List<int> leftList = GetLeftList(lines);
            List<int> rightList = GetRightList(lines);

            var similarity = 0;

            // For every number in the left list
            foreach (var number in leftList)
            {
                // Get how many times the number appears in the right list
                var multiplier = rightList.Where(n => n == number).Count();

                // Increase the similarity by adding the value of the
                // number on the left when multiplied by how many times
                // it appears on the right
                similarity += number * multiplier;
            }

            return similarity.ToString();
        }

        private static List<int> GetLeftList(List<string> lines)
        {
            List<int> list = new List<int>();

            // For each line
            foreach (var line in lines)
            {
                // Get the line up to the first space
                var number = line.Substring(0, line.IndexOf(" "));

                // Parse the line as a number
                list.Add(int.Parse(number));
            }

            return list;
        }

        private static List<int> GetRightList(List<string> lines)
        {
            List<int> list = new List<int>();

            // For each line
            foreach (var line in lines)
            {
                // Get the line after the first space
                var number = line.Substring(line.IndexOf(" "));

                // Parse the line as a number, removing any whitespace
                list.Add(int.Parse(number.Trim()));
            }

            return list;
        }
    }
}
