using System.Collections.Generic;
namespace ScaffoldUI.Fixed.Schemas;

/// <summary>
/// Parsed layout data ready for computation.
/// Each field contains a list of LayoutValue to support percent + pixel offsets.
/// Margin is separated into top, right, bottom, left.
/// </summary>
public class ParsedLayoutData
{
    public List<LayoutValue> Width { get; set; }
    public List<LayoutValue> Height { get; set; }
    public List<LayoutValue> X { get; set; }
    public List<LayoutValue> Y { get; set; }


    public List<LayoutValue> MarginTop { get; set; }
    public List<LayoutValue> MarginRight { get; set; }
    public List<LayoutValue> MarginBottom { get; set; }
    public List<LayoutValue> MarginLeft { get; set; }


    /// <summary>
    /// Constructor initializes all lists.
    /// </summary>
    public ParsedLayoutData()
    {
        Width = [];
        Height = [];
        X = [];
        Y = [];

        MarginTop = [];
        MarginRight = [];
        MarginBottom = [];
        MarginLeft = [];
    }
}