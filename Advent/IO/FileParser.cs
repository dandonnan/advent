namespace Advent.IO
{
    internal class FileParser
    {
        public static string ReadInputFile(string fileName, int year)
        {
            string file = $"{AppDomain.CurrentDomain.BaseDirectory}//..//..//..//..//Input//{year}//{fileName}.txt";

            using (StreamReader streamReader = new StreamReader(file))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static List<string> ReadInputFileAsLines(string fileName, int year)
        {
            List<string> lines = new List<string>();

            string file = $"{AppDomain.CurrentDomain.BaseDirectory}//..//..//..//..//Input//{year}//{fileName}.txt";

            using (StreamReader streamReader = new StreamReader(file))
            {
                while (streamReader.EndOfStream == false)
                {
                    lines.Add(streamReader.ReadLine());
                }
            }

            return lines;
        }
    }
}
