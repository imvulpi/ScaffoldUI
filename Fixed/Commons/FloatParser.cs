using Godot;
using System;
using System.Runtime.CompilerServices;

namespace ScaffoldUI.Fixed.Commons
{
    public static class FloatParser
    {
        /// <summary>
        /// Array of inverted powers of ten
        /// </summary>
        static readonly float[] invPow10 =
        [
            1.0F,
            1e-1F,
            1e-2F,
            1e-3F,
            1e-4F,
            1e-5F,
            1e-6F,
            1e-7F,
            1e-8F,
            1e-9F,
            1e-10F,
            1e-11F,
            1e-12F,
            1e-13F,
            1e-14F,
            1e-15F,
            1e-16F
        ];

        /// <summary>
        /// Parses a given <paramref name="span"/> to a float.
        /// </summary>
        /// <remarks>
        /// Only treats dots as decimal separators and skips spaces.
        /// </remarks>
        /// <param name="span"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Parse(ReadOnlySpan<char> span)
        {
            float result = 0;
            int i = 0;
            int sign = 1;
            char c;

            while (i < span.Length)
            {
                c = span[i];
                if (c == ' ')
                {
                    i++;
                    continue;
                }
                else if (c == '-')
                {
                    i++;
                    sign = -1;
                    break;
                }
                else if (c == '+')
                {
                    i++;
                    break;
                }

                break;
            }

            while (i < span.Length && span[i] != '.')
            {
                c = span[i++];
                if (c == ' ') continue;
                result = result * 10 + (c - '0');
            }

            if (i < span.Length && span[i] == '.')
            {
                i++;
                long frac = 0;
                byte amount = 0;
                while (i < span.Length)
                {
                    c = span[i++];
                    if (c == ' ') continue;
                    frac = frac * 10 + (c - '0');
                    amount += 1;
                }
                result += frac * invPow10[amount];
            }

            return result * sign;
        }
    }
}
