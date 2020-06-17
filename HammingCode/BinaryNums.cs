using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace HammingCode
{
    struct Bnum
    {
        private int _value;
        //public int Value { get { return _value; } 
        //    set {
        //        if (value > 1)
        //            _value = value % 2;
        //        if (value == 0 || value == 1)
        //            _value = value;
        //        else
        //        {
        //            _value = Math.Abs(value) % 2;
        //        }
        //    }
        //}
        public Bnum(int value)
        {
            if (value > 1)
                _value = value % 2;
            if (value == 0 || value == 1)
                _value = value;
            else
            {
                _value = Math.Abs(value) % 2;
            }
        }

        #region Operators
        public static Bnum operator +(Bnum b1, Bnum b2)
        {
            return new Bnum((b1._value + b2._value) % 2);
        }
        public static Bnum operator -(Bnum b1, Bnum b2)
        {
            return new Bnum(Math.Abs((b1._value - b2._value)) % 2);
        }
        public static Bnum operator *(Bnum b1, Bnum b2)
        {
            return new Bnum(b1._value * b2._value);
        }

        public static bool operator true(Bnum b1) => b1._value == 1;
        public static bool operator false(Bnum b1) => b1._value == 0;
        public static bool operator !(Bnum b1)
        {
            return b1._value == 0 ? true : false;
        }
        public static bool operator >(Bnum b1, Bnum b2)
        {
            return b1._value > b2._value;
        }
        public static bool operator >=(Bnum b1, Bnum b2)
        {
            return b1._value >= b2._value;
        }
        public static bool operator <=(Bnum b1, Bnum b2)
        {
            return b1._value <= b2._value;
        }
        public static bool operator ==(Bnum b1, Bnum b2)
        {
            return b1._value == b2._value;
        }
        public static bool operator ==(int b1, Bnum b2)
        {
            return b1 == b2._value;
        }
        public static bool operator !=(int b1, Bnum b2)
        {
            return b1 != b2._value;
        }
        public static bool operator ==(Bnum b1, int b2)
        {
            return b1._value == b2;
        }
        public static bool operator !=(Bnum b1, int b2)
        {
            return b1._value != b2;
        }
        public static bool operator !=(Bnum b1, Bnum b2)
        {
            return b1._value != b2._value;
        }
        public static bool operator <(Bnum b1, Bnum b2)
        {
            return b1._value < b2._value;
        }

        #endregion

        public override string ToString()
        {
            return $"{_value}";
        }
    }
}
