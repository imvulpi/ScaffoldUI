using Godot;
using ScaffoldUI.Fixed.Commons;
using System;

namespace ScaffoldUI.Fixed.Schemas
{
    public class LayoutParser
    {
        /// <summary>
        /// Parses all values from the <paramref name="data"/>.
        /// </summary>
        public static ParsedLayoutData ParseLayout(LayoutData data)
        {
            ParsedLayoutData parsed = new ParsedLayoutData
            {
                Width = [.. ParseDimension(data.Width)],
                Height = [.. ParseDimension(data.Height)],
                X = [.. ParseDimension(data.X)],
                Y = [.. ParseDimension(data.Y)]
            };

            ParseMargin(data.Margin, parsed);

            return parsed;
        }

        /// <summary>
        /// Parses a single dimension string into a list of LayoutValue objects.
        /// Supports multiple values (ex: "50% + 10px").
        /// </summary>
        /// <remarks>
        /// The input should not contain '(' or ')' (there is no depth in one dimension)
        /// </remarks>
        /// <returns>List of LayoutValue representing the dimension value.</returns
        public static LayoutValue[] ParseDimension(ReadOnlySpan<char> input)
        {
            LayoutValue[] values = new LayoutValue[2];
            int start = 0;
            int numberEnd = 0;
            int index = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (char.IsDigit(c) || c == '.' || c == ' ')
                {
                    continue;
                }

                if (c == '-' || c == '+')
                {
                    if (index >= values.Length)
                    {
                        LayoutValue[] valuesGrown = new LayoutValue[values.Length * 2];
                        Array.Copy(values, valuesGrown, values.Length);
                        values = valuesGrown;
                    }
                    LayoutValue? value = ParseValue(input.Slice(start, i - start), numberEnd - start);
                    if (value != null) values[index] = (LayoutValue)value;
                    index++;
                    numberEnd = 0;
                    start = i;
                    continue;
                }

                if (numberEnd == 0)
                {
                    numberEnd = i;
                }
            }

            LayoutValue? finalValue = ParseValue(input.Slice(start), numberEnd - start);
            if (finalValue != null) values[index] = (LayoutValue)finalValue;

            return values;
        }

        /// <summary>
        /// Parses the float number in <paramref name="input"/> from index 0 to <paramref name="numberEndIndex"/>.
        /// Treats the remaining string as unit.
        /// </summary>
        /// <remarks>
        /// The unit part should not contain any spaces or dots, the correct unit wont get detected.
        /// </remarks>
        /// <returns>Parsed value or null if bad input.</returns>
        public static LayoutValue? ParseValue(ReadOnlySpan<char> input, int numberEndIndex)
        {
            if (input == null) return null;
            if (numberEndIndex == 0) return null;

            float number = FloatParser.Parse(input[..numberEndIndex]);
            ReadOnlySpan<char> unit = input[numberEndIndex..];
            switch (unit)
            {
                case Units.UNIT_PX:
                    return new(UnitType.Pixels, number);
                case Units.UNIT_PERCENT:
                    return new(UnitType.Percent, number);
                default:
                    GD.PushWarning($"Failed to value unit '{unit}'. Interpreting it as px instead");
                    return new(UnitType.Pixels, number);
            }
        }

        /// <summary>
        /// Parses a CSS-like margin string and populates the <see cref="ParsedLayoutData"/> with values for each side.
        /// </summary>
        /// <param name="input">
        /// The input string representing the margin. Supports 1 to 4 shorthand values, e.g. "10px 20% 30px 40px".
        /// Single-level parenthesized expressions with +/- are supported, e.g. "(10px - 5%)".
        /// </param>
        /// <param name="parsedLayout">
        /// The <see cref="ParsedLayoutData"/> instance to populate with the parsed margin values.
        /// Each of MarginTop, MarginRight, MarginBottom, MarginLeft will contain a list of <see cref="LayoutValue"/>.
        /// </param>
        /// <remarks>
        /// Shorthand expansion follows CSS rules:
        /// - 1 value = all sides
        /// - 2 values = top/bottom, left/right
        /// - 3 values = top, left/right, bottom
        /// - 4 values = top, right, bottom, left
        /// </remarks>
        public static void ParseMargin(ReadOnlySpan<char> input, ParsedLayoutData parsedLayout)
        {
            LayoutValue[][] marginValues = new LayoutValue[4][];
            input = input.Trim();
            bool openBracket = false;
            int start = 0;
            int index = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '(')
                {
                    start++;
                    openBracket = true;
                    continue;
                }

                if (c == ')')
                {
                    openBracket = false;
                    continue;
                }

                if (c == ' ' && !openBracket)
                {
                    if (input[i - start] == ')') marginValues[index] = ParseDimension(input.Slice(start, i - start - 1)); // -1 to ignore the ')'
                    else marginValues[index] = ParseDimension(input.Slice(start, i - start));
                    start = i + 1;
                    index++;
                    continue;
                }
            }

            if (input[^1] == ')') input = input.Slice(0, input.Length - 1);
            marginValues[index] = ParseDimension(input.Slice(start));

            switch (marginValues.Length)
            {
                case 0:
                    return;
                case 1:
                    parsedLayout.MarginLeft = [.. marginValues[0]];
                    parsedLayout.MarginRight = [.. marginValues[0]];
                    parsedLayout.MarginTop = [.. marginValues[0]];
                    parsedLayout.MarginBottom = [.. marginValues[0]];
                    return;
                case 2:
                    parsedLayout.MarginTop = [.. marginValues[0]];
                    parsedLayout.MarginBottom = [.. marginValues[0]];
                    parsedLayout.MarginLeft = [.. marginValues[1]];
                    parsedLayout.MarginRight = [.. marginValues[1]];
                    return;
                case 3:
                    parsedLayout.MarginTop = [.. marginValues[0]];
                    parsedLayout.MarginLeft = [.. marginValues[1]];
                    parsedLayout.MarginRight = [.. marginValues[1]];
                    parsedLayout.MarginBottom = [.. marginValues[2]];
                    return;
                case 4:
                    parsedLayout.MarginTop = [.. marginValues[0]];
                    parsedLayout.MarginRight = [.. marginValues[1]];
                    parsedLayout.MarginBottom = [.. marginValues[2]];
                    parsedLayout.MarginLeft = [.. marginValues[3]];
                    return;
            }
        }
    }
}
