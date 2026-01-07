namespace ScaffoldUI.Fixed.Schemas;

/// <summary>
/// Represents the layout data for a Control node, including size, position, and margins.
/// All values are stored as strings to allow percentage and pixel offsets (e.g., "40% + 12px").
/// </summary>
public class LayoutData
{
    /// <summary>
    /// The width of the Control. Example: "50% + 10px".
    /// </summary>
    public string Width { get; set; }

    /// <summary>
    /// The height of the Control. Example: "30% + 5px".
    /// </summary>
    public string Height { get; set; }

    /// <summary>
    /// The X position of the Control relative to its parent. Example: "50%".
    /// </summary>
    public string X { get; set; }

    /// <summary>
    /// The Y position of the Control relative to its parent. Example: "70% + 8px".
    /// </summary>
    public string Y { get; set; }

    /// <summary>
    /// Margin in the shorthand format. Can be specified in pixels or percentages. Example: "5% 2px 10% 4px".
    /// </summary>
    public string Margin { get; set; }

    /// <summary>
    /// Constructor to initialize LayoutData with optional default values.
    /// </summary>
    public LayoutData()
    {
        Width = "0%";
        Height = "0%";
        X = "0%";
        Y = "0%";
        Margin = "0px";
    }
}
