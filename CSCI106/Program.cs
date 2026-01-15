namespace CSCI106
{
    internal class Program
    {
        static void Main()
        {
            var svg = SvgBuilder.New((500, 500)).Build();

            Console.Write("Absolute path to save SVG at: ");
            var path = Console.ReadLine() ?? "";

            using (var fileWriter = FileWriter.FromAbsolutePath(path))
                fileWriter.WriteLine(svg);
        }
    }
}
