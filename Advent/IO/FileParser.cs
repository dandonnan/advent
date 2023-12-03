namespace Advent.IO
{
    internal class FileParser
    {
        public static string ReadInputFile(string fileName)
        {
            string file = $"{AppDomain.CurrentDomain.BaseDirectory}//..//..//..//..//Input//{fileName}.txt";

            using (StreamReader streamReader = new StreamReader(file))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static List<string> ReadInputFileAsLines(string fileName)
        {
            List<string> lines = new List<string>();

            string file = $"{AppDomain.CurrentDomain.BaseDirectory}//..//..//..//..//Input//{fileName}.txt";

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
