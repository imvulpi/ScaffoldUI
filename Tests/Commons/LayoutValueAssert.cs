using ScaffoldUI.Fixed.Schemas;
using System;

namespace GdUnit4
{
    public class LayoutValueAssert(LayoutValue current)
    {
        LayoutValue Current { get; set; } = current;

        public LayoutValueAssert IsEqual(float value, UnitType unit)
        {
            bool valueNotEqual = Current.Value != value;
            bool unitNotEqual = Current.Unit != unit;
            if (valueNotEqual && unitNotEqual)
            {
                throw new Exception($"Expecting be equal:\n{Current.Value} != {value}\n{Current.Unit} != {unit}");
            }
            else if (valueNotEqual)
            {
                throw new Exception($"Expecting be equal:\n{Current.Value} != {value}");
            }
            else if(unitNotEqual)
            {
                throw new Exception($"Expecting be equal:\n{Current.Unit} != {unit}");
            }

            return this;
        }

        public LayoutValueAssert IsEqual(LayoutValue expected)
        {
            bool valueNotEqual = Current.Value != expected.Value;
            bool unitNotEqual = Current.Unit != expected.Unit;
            if (valueNotEqual && unitNotEqual)
            {
                throw new Exception($"Expecting be equal:\n{Current.Value} != {expected.Value}\n{Current.Unit} != {expected.Unit}");
            }
            else if (valueNotEqual)
            {
                throw new Exception($"Expecting be equal:\n{Current.Value} != {expected.Value}");
            }
            else if (unitNotEqual)
            {
                throw new Exception($"Expecting be equal:\n{Current.Unit} != {expected.Unit}");
            }
            return this;
        }

        public LayoutValueAssert IsNotEqual(LayoutValue expected)
        {
            bool valueEqual = Current.Value == expected.Value;
            bool unitEqual = Current.Unit == expected.Unit;
            if (valueEqual && unitEqual)
            {
                throw new Exception($"Expecting be not equal:\n{Current.Value} == {expected.Value}\n{Current.Unit} == {expected.Unit}");
            }
            else if (valueEqual)
            {
                throw new Exception($"Expecting be not equal:\n{Current.Value} == {expected.Value}");
            }
            else if (unitEqual)
            {
                throw new Exception($"Expecting be not equal:\n{Current.Unit} == {expected.Unit}");
            }

            return this;
        }
        
        public LayoutValueAssert IsNotEqual(float value, UnitType unit)
        {
            bool valueEqual = Current.Value == value;
            bool unitEqual = Current.Unit == unit;
            if (valueEqual && unitEqual)
            {
                throw new Exception($"Expecting be not equal:\n{Current.Value} == {value}\n{Current.Unit} == {unit}");
            }
            else if (valueEqual)
            {
                throw new Exception($"Expecting be not equal:\n{Current.Value} == {value}");
            }
            else if (unitEqual)
            {
                throw new Exception($"Expecting be not equal:\n{Current.Unit} == {unit}");
            }

            return this;
        }

        public LayoutValueAssert IsNotNull()
        {
            if (Current.Equals(null))
            {
                throw new Exception("Expecting be not null:\nit's null");
            }
            return this;
        }

        public LayoutValueAssert IsNull()
        {
            if (Current.Equals(null))
            {
                return this;
            }
            throw new Exception($"Expecting be null:\n{Current}");
        }
    }
}
