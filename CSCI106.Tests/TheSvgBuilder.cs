namespace CSCI106.Tests
{
    public class TheSvgBuilder
    {
        [Test]
        public void BuildsSvgsWithTheCorrectSize()
        {
            var svg = SvgBuilder.New((123, 456)).Build();

            Assert.That(svg, Contains.Substring("width=\"123\""));
            Assert.That(svg, Contains.Substring("height=\"456\""));
        }
    }
}
