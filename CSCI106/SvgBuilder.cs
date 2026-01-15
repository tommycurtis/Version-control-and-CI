namespace CSCI106
{
    public class SvgBuilder
    {
        private const string SVG_HEADER_TEMPLATE = "<svg width=\"{0}\" height=\"{1}\" xmlns=\"http://www.w3.org/2000/svg\">";
        private const string SVG_FOOTER = "</svg>";

        private string Buffer;
        private uint Width;
        private uint Height;

        public static SvgBuilder New((uint, uint) dimensions)
        {
            var (width, height) = dimensions;

            return new()
            {
                Buffer = string.Empty,
                Width = width,
                Height = height,
            };
        }

        public string Build() =>
            string.Format(SVG_HEADER_TEMPLATE, Width, Height)
                + Buffer
                + SVG_FOOTER;
    }
}
