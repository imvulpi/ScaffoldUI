namespace ScaffoldUI.Fixed.Schemas;

/// <summary>
/// Represents a single numeric value with an associated unit (percent or pixels).
/// </summary>
public struct LayoutValue
{
    /// <summary>
    /// Unit type: Percent or Pixels.
    /// </summary>
    public UnitType Unit { get; set; }

    /// <summary>
    /// Numeric value for the layout dimension.
    /// </summary>
    public float Value { get; set; }

    public LayoutValue(UnitType unit, float value)
    {
        Unit = unit;
        Value = value;
    }

    public override string ToString()
    {
        return Unit switch
        {
            UnitType.Percent => $"{Value}%",
            UnitType.Pixels => $"{Value}px",
            _ => $"{Value}?",
        };
    }
}
