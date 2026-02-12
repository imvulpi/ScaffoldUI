using BenchmarkDotNet.Attributes;
using ScaffoldUI.Fixed.Schemas;
using System.Collections.Generic;
namespace ScaffoldUI.Benchmarks.Layout;

public class LayoutParserBenchmarks
{
    private string dimension;
    private string margin;
    private string value;
    private ParsedLayoutData parsedLayoutData;
    private LayoutData layoutData;

    [GlobalSetup]
    public void Setup()
    {
        dimension = "10px + 10%";
        margin = "(10px + 5%)";
        value = "10px";

        layoutData = new LayoutData
        {
            Height = "100px + 5%",
            Width = "50% + 20px",
            X = "10px",
            Y = "20px",
            Margin = "10px 5%"
        };

        parsedLayoutData = new ParsedLayoutData();
    }

    [Benchmark]
    public List<LayoutValue> ParseDimensionBenchmark()
    {
        return LayoutParser.ParseDimension(dimension);
    }

    [Benchmark]
    public void ParseMarginBenchmark()
    {
        LayoutParser.ParseMargin(margin, parsedLayoutData);
    }

    [Benchmark]
    public ParsedLayoutData ParseLayoutBenchmark()
    {
        return LayoutParser.ParseLayout(layoutData);
    }

    [Benchmark]
    public LayoutValue? ParseValueBenchmark()
    {
        return LayoutParser.ParseValue(value, 2);
    }
}
