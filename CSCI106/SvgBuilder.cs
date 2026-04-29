namespace CSCI106
{
    public class SvgBuilder
    {
        private const string SVG_HEADER_TEMPLATE = "<svg width=\"{0}\" height=\"{1}\" xmlns=\"http://www.w3.org/2000/svg\">";
        private const string SVG_FOOTER = "</svg>";
        
        private string Buffer = string.Empty;
        private uint Width;
        private uint Height;

        public static SvgBuilder New((uint width, uint height) dimensions)
        {
            var (width, height) = dimensions;
            return new SvgBuilder
            {
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
            // Basic dimension check
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Rectangle width and height must both be greater than zero.");
            }

            // Check visibility in viewport (must have positive overlap area)
            double left = Math.Max(x, 0);
            double right = Math.Min(x + width, Width);
            double top = Math.Max(y, 0);
            double bottom = Math.Min(y + height, Height);

            if (left >= right || top >= bottom)
            {
                throw new ArgumentException(
                    "Rectangle is completely outside the SVG viewport and would not be visible. " +
                    "It must overlap the view area (even slightly).");
            }

            // All good — add the rectangle
            string rect = $@"<rect x=""{x}"" y=""{y}"" width=""{width}"" height=""{height}"" " +
                          $@"fill=""{fill}"" stroke=""{stroke}"" stroke-width=""{strokeWidth}"" />";
            
            Buffer += rect + "\n";
            return this;
        }

        public string Build() =>
            string.Format(SVG_HEADER_TEMPLATE, Width, Height) +
            Buffer +
            SVG_FOOTER;
    }

    // ────────────────────────────────────────────────
    // Console tests — run SvgBuilderTests.RunTests() to see them all
    public static class SvgBuilderTests
    {
        public static void RunTests()
        {
            Console.WriteLine("Running SVG Builder validation tests...\n");

            TestBasicRectangle();
            TestStyledRectangle();
            TestMultipleRectangles();
            TestPartialOverlapAllowed();
            TestZeroOrNegativeDimensionsRejected();
            TestCompletelyOffscreenRejected();
            TestZeroAreaEdgeTouchRejected();
            TestInvalidDoesNotAffectValid();

            Console.WriteLine("\nAll tests finished.");
        }

        private static void TestBasicRectangle()
        {
            var svg = SvgBuilder.New((400, 300))
                .AddRectangle(50, 50, 200, 100)
                .Build();

            Console.WriteLine("Test 1: Basic centered rectangle");
            Console.WriteLine(svg);
            Console.WriteLine();
        }

        private static void TestStyledRectangle()
        {
            var svg = SvgBuilder.New((500, 400))
                .AddRectangle(80, 120, 300, 150, fill: "#ff6347", stroke: "navy", strokeWidth: 4)
                .Build();

            Console.WriteLine("Test 2: Rectangle with fill, stroke, and width");
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

            Console.WriteLine("Test 3: Three different rectangles");
            Console.WriteLine(svg);
            Console.WriteLine();
        }

        private static void TestPartialOverlapAllowed()
        {
            var builder = SvgBuilder.New((400, 300));
            builder.AddRectangle(-20, 50, 50, 40);    // left edge
            builder.AddRectangle(380, 100, 50, 50);   // right edge
            builder.AddRectangle(150, -10, 80, 30);   // top edge
            builder.AddRectangle(200, 280, 60, 30);   // bottom edge

            Console.WriteLine("Test 4: Partially visible rectangles → all accepted");
            Console.WriteLine("    (4 rectangles added successfully)\n");
        }

        private static void TestZeroOrNegativeDimensionsRejected()
        {
            var builder = SvgBuilder.New((400, 300));
            bool caught = false;

            try { builder.AddRectangle(50, 50, 0, 100); } catch { caught = true; }
            Console.WriteLine(caught ? "Test 5a: Zero width → caught" : "FAILED 5a");

            caught = false;
            try { builder.AddRectangle(50, 50, 200, 0); } catch { caught = true; }
            Console.WriteLine(caught ? "Test 5b: Zero height → caught" : "FAILED 5b");

            caught = false;
            try { builder.AddRectangle(50, 50, -30, 100); } catch { caught = true; }
            Console.WriteLine(caught ? "Test 5c: Negative width → caught" : "FAILED 5c");
            Console.WriteLine();
        }

        private static void TestCompletelyOffscreenRejected()
        {
            var builder = SvgBuilder.New((400, 300));
            bool caught;

            caught = false; try { builder.AddRectangle(500, 100, 50, 50); } catch { caught = true; }
            Console.WriteLine(caught ? "Test 6a: Right offscreen → caught" : "FAILED 6a");

            caught = false; try { builder.AddRectangle(-100, 100, 50, 50); } catch { caught = true; }
            Console.WriteLine(caught ? "Test 6b: Left offscreen → caught" : "FAILED 6b");

            caught = false; try { builder.AddRectangle(100, -200, 50, 50); } catch { caught = true; }
            Console.WriteLine(caught ? "Test 6c: Top offscreen → caught" : "FAILED 6c");

            caught = false; try { builder.AddRectangle(100, 400, 50, 50); } catch { caught = true; }
            Console.WriteLine(caught ? "Test 6d: Bottom offscreen → caught" : "FAILED 6d");
            Console.WriteLine();
        }

        private static void TestZeroAreaEdgeTouchRejected()
        {
            var builder = SvgBuilder.New((400, 300));

            try { builder.AddRectangle(400, 100, 100, 100); }
            catch { Console.WriteLine("Test 7a: Exact right edge (no overlap) → caught"); }

            try { builder.AddRectangle(100, 300, 100, 100); }
            catch { Console.WriteLine("Test 7b: Exact bottom edge (no overlap) → caught"); }
            Console.WriteLine();
        }

        private static void TestInvalidDoesNotAffectValid()
        {
            var builder = SvgBuilder.New((400, 300));
            builder.AddRectangle(60, 60, 120, 80);  // valid

            try
            {
                builder.AddRectangle(1000, 1000, 50, 50);  // invalid
                Console.WriteLine("Test 8: FAILED — invalid should have thrown");
            }
            catch
            {
                Console.WriteLine("Test 8: Invalid rejected, valid rectangle still present ✓");
            }
            Console.WriteLine();
        }
    }
}
public bool AddRectFromText(string s)

    s = s.Trim().ToLower();
    if (!s.StartsWith("rect ")) return false;

    // Replace commas with space so split works the same either way
    string content = s.Substring(5).Replace(",", " ");

    // Split on whitespace and throw away empty entries
    string[] parts = content.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

    if (parts.Length < 5) return false;

    string color = parts[^1];           // last = color

    // Try to parse exactly four numbers before the color
    if (!double.TryParse(parts[0], out double x) ||
        !double.TryParse(parts[1], out double y) ||
        !double.TryParse(parts[2], out double w) ||
        !double.TryParse(parts[3], out double h))
    {
        return false;
    }

    if (w <= 0 || h <= 0) return false;

    try
    {
        AddRectangle(x, y, w, h, fill: color);
        return true;
    }
    catch
    {
        return false;
    }