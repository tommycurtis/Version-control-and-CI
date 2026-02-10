namespace CSCI106
{
    public class SvgBuilder
    {
        private const string SVG_HEADER_TEMPLATE = "<svg width=\"{0}\" height=\"{1}\" xmlns=\"http://www.w3.org/2000/svg\">";
        private const string SVG_FOOTER = "</svg>";
        
        private string Buffer;
        private uint Width;
        private uint Height;

        public static SvgBuilder New((uint width, uint height) dimensions)
        {
            var (width, height) = dimensions;
            return new SvgBuilder
            {
                Buffer = string.Empty,
                Width = width,
                Height = height
            };
        }

        
        public SvgBuilder AddRectangle(
            double x, 
            double y, 
            double width, 
            double height,
            string fill = "black",
            string stroke = "none",
            double strokeWidth = 1.0)
        {
            string rect = $@"<rect x=""{x}"" y=""{y}"" width=""{width}"" height=""{height}"" fill=""{fill}"" stroke=""{stroke}"" stroke-width=""{strokeWidth}"" />";
            Buffer += rect + "\n";  
            return this;  
        }

        public string Build() =>
            string.Format(SVG_HEADER_TEMPLATE, Width, Height)
            + Buffer
            + SVG_FOOTER;
    }

    // ────────────────────────────────────────────────
    // Simple console-based tests (you can put these in a test project or run them in Main)
    public static class SvgBuilderTests
    {
        public static void RunTests()
        {
            TestBasicRectangle();
            TestRectangleWithStrokeAndColor();
            TestMultipleRectangles();
            Console.WriteLine("All tests completed.");
        }

        private static void TestBasicRectangle()
        {
            var svg = SvgBuilder.New((400, 300))
                .AddRectangle(50, 50, 200, 100)  //
                .Build();

            Console.WriteLine("=== Test 1: Basic Rectangle ===");
            Console.WriteLine(svg);
            Console.WriteLine();

            
            
        }

        private static void TestRectangleWithStrokeAndColor()
        {
            var svg = SvgBuilder.New((500, 400))
                .AddRectangle(80, 120, 300, 150, fill: "#ff6347", stroke: "navy", strokeWidth: 4)
                .Build();

            Console.WriteLine("=== Test 2: Rectangle with custom fill & stroke ===");
            Console.WriteLine(svg);
            Console.WriteLine();

            
        }

        private static void TestMultipleRectangles()
        {
            var svg = SvgBuilder.New((600, 400))
                .AddRectangle(30, 40, 120, 80, fill: "lime")
                .AddRectangle(200, 150, 180, 100, fill: "blue", stroke: "yellow", strokeWidth: 6)
                .AddRectangle(400, 220, 140, 90, fill: "#ff69b4")
                .Build();

            Console.WriteLine("=== Test 3: Multiple Rectangles ===");
            Console.WriteLine(svg);
            Console.WriteLine();
        }
    }
}
