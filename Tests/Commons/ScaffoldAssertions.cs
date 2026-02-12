using ScaffoldUI.Fixed.Schemas;
using System;
using System.Diagnostics;

namespace GdUnit4
{
    public static class ScaffoldAssertions
    {
        public static LayoutValueAssert AssertLayoutValue(LayoutValue layoutValue)
        {
            return new LayoutValueAssert(layoutValue);
        }
    }
}
