using GdUnit4;
using ScaffoldUI.Fixed.Schemas;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

[TestSuite]
public class LayoutParserTests
{
    #region ParseValue

    [TestCase("10px", 2, 10)]
    [TestCase("20%", 2, 20)]
    public void ParseValue_ShouldParseSingleNumber(string input, int numberEndIndex, float expected)
    {
        var result = LayoutParser.ParseValue(input.AsSpan(), numberEndIndex);

        Assertions.AssertObject(result).IsNotNull();
        Assertions.AssertFloat(result!.Value.Value).IsEqual(expected);
    }

    [TestCase]
    public void ParseValue_ShouldReturnNull_ForInvalidInput()
    {
        var result = LayoutParser.ParseValue("abc".AsSpan(), 0);
        Assertions.AssertObject(result).IsNull();
    }

    #endregion

    #region ParseDimension

    [TestCase]
    public void ParseDimension_ShouldParseSimplePx()
    {
        var values = LayoutParser.ParseDimension("10px".AsSpan());

        Assertions.AssertInt(values.Count).IsEqual(1);
        Assertions.AssertFloat(values[0].Value).IsEqual(10);
        Assertions.AssertObject(values[0].Unit).IsEqual(UnitType.Pixels);
    }

    [TestCase]
    public void ParseDimension_ShouldParseMixedUnits()
    {
        var values = LayoutParser.ParseDimension("10px + 10%".AsSpan());

        Assertions.AssertInt(values.Count).IsEqual(2);
        ScaffoldAssertions.AssertLayoutValue(values[0]).IsEqual(10, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(values[1]).IsEqual(10, UnitType.Percent);
    }

    [TestCase]
    public void ParseDimension_ShouldHandleWhitespace()
    {
        var values = LayoutParser.ParseDimension("  5px   +   15% ".AsSpan());
        Assertions.AssertInt(values.Count).IsEqual(2);
        ScaffoldAssertions.AssertLayoutValue(values[0]).IsEqual(5, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(values[1]).IsEqual(15, UnitType.Percent);
    }

    #endregion

    #region ParseMargin

    [TestCase]
    public void ParseMargin_SingleValue_AppliesToAllSides()
    {
        var data = new ParsedLayoutData();
        LayoutParser.ParseMargin("10px".AsSpan(), data);
        ScaffoldAssertions.AssertLayoutValue(data.MarginTop[0]).IsEqual(10, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(data.MarginBottom[0]).IsEqual(10, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(data.MarginLeft[0]).IsEqual(10, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(data.MarginRight[0]).IsEqual(10, UnitType.Pixels);
    }

    [TestCase]
    public void ParseMargin_TwoValues_HorizontalVertical()
    {
        var data = new ParsedLayoutData();
        LayoutParser.ParseMargin("10px 20%".AsSpan(), data);

        ScaffoldAssertions.AssertLayoutValue(data.MarginTop[0]).IsEqual(10, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(data.MarginBottom[0]).IsEqual(10, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(data.MarginLeft[0]).IsEqual(20, UnitType.Percent);
        ScaffoldAssertions.AssertLayoutValue(data.MarginRight[0]).IsEqual(20, UnitType.Percent);
    }

    [TestCase]
    public void ParseMargin_MixedUnits_Works()
    {
        var data = new ParsedLayoutData();
        LayoutParser.ParseMargin("(10% + 50px) 5px".AsSpan(), data);

        // Margin Top:
        Assertions.AssertInt(data.MarginTop.Count).IsEqual(2);
        ScaffoldAssertions.AssertLayoutValue(data.MarginTop[0]).IsEqual(10, UnitType.Percent);
        ScaffoldAssertions.AssertLayoutValue(data.MarginTop[1]).IsEqual(50, UnitType.Pixels);

        // Margin Bottom:
        Assertions.AssertInt(data.MarginBottom.Count).IsEqual(2);
        ScaffoldAssertions.AssertLayoutValue(data.MarginBottom[0]).IsEqual(10, UnitType.Percent);
        ScaffoldAssertions.AssertLayoutValue(data.MarginBottom[1]).IsEqual(50, UnitType.Pixels);

        // Remaining:
        Assertions.AssertInt(data.MarginLeft.Count).IsEqual(1);
        Assertions.AssertInt(data.MarginRight.Count).IsEqual(1);
        ScaffoldAssertions.AssertLayoutValue(data.MarginLeft[0]).IsEqual(5, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(data.MarginRight[0]).IsEqual(5, UnitType.Pixels);
    }

    #endregion

    #region ParseLayout

    [TestCase]
    public void ParseLayout_FullLayout_Works()
    {
        var layout = new LayoutData
        {
            Width = "100%",
            Height = "200px",
            X = "10px + 5%",
            Y = "5px",
            Margin = "10px 5px"
        };

        var result = LayoutParser.ParseLayout(layout);

        // Checking class:
        Assertions.AssertObject(result).IsNotNull();
        
        // Checking the width:
        Assertions.AssertObject(result.Width).IsNotNull();
        Assertions.AssertInt(result.Width.Count).IsEqual(1);
        ScaffoldAssertions.AssertLayoutValue(result.Width[0]).IsEqual(100, UnitType.Percent);
        
        // Checking the height:
        Assertions.AssertObject(result.Height).IsNotNull();
        Assertions.AssertInt(result.Height.Count).IsEqual(1);
        ScaffoldAssertions.AssertLayoutValue(result.Height[0]).IsEqual(200, UnitType.Pixels);

        // Checking the X:
        Assertions.AssertObject(result.X).IsNotNull();
        Assertions.AssertInt(result.X.Count).IsEqual(2);
        ScaffoldAssertions.AssertLayoutValue(result.X[0]).IsEqual(10, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(result.X[1]).IsEqual(5, UnitType.Percent);

        // Checking the Y:
        Assertions.AssertObject(result.Y).IsNotNull();
        Assertions.AssertInt(result.Y.Count).IsEqual(1);
        ScaffoldAssertions.AssertLayoutValue(result.Y[0]).IsEqual(5, UnitType.Pixels);

        // Checking margins:
        ScaffoldAssertions.AssertLayoutValue(result.MarginTop[0]).IsEqual(10, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(result.MarginBottom[0]).IsEqual(10, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(result.MarginLeft[0]).IsEqual(5, UnitType.Pixels);
        ScaffoldAssertions.AssertLayoutValue(result.MarginRight[0]).IsEqual(5, UnitType.Pixels);
    }

    #endregion 
}