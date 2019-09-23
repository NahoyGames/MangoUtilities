using System;
using System.Collections.Generic;

namespace MangoUtilities
{
    public struct Int24 : IEquatable<Int24>
    {
        private readonly int _value;
        
        private Int24(int value)
        {
            if (value >= (1 << 24))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            _value = value;
        }
        
        /* Serialization */
        public byte[] ToBytes() => new[] { (byte)(_value), (byte)(_value >> 8), (byte)(_value >> 16) };

        public static Int24 FromBytes(byte one, byte two, byte three) => one + (two << 8) + (three << 16);

        public static Int24 FromBytes(byte[] value) => FromBytes(value[0], value[1], value[2]);
        
        /* Object */
        public override string ToString() => _value.ToString();

        public override bool Equals(object obj) => obj != null && obj is Int24 int24 && int24._value == _value;

        public override int GetHashCode() => EqualityComparer<Int24>.Default.GetHashCode(_value);

        /* IEquatable */
        public bool Equals(Int24 other) => _value == other._value;
        
        /* Operators */
        public static implicit operator Int24(int value) => new Int24(value);

        public static implicit operator int(Int24 me) => me._value;

        public static bool operator <(Int24 a, Int24 b) => a._value < b._value;
        
        public static bool operator >(Int24 a, Int24 b) => a._value > b._value;

        public static bool operator <=(Int24 a, Int24 b) => (a._value < b._value) || (a._value == b._value);
        
        public static bool operator >=(Int24 a, Int24 b) => (a._value > b._value) || (a._value == b._value);
        
        public static bool operator ==(Int24 a, Int24 b) => a._value == b._value;

        public static bool operator !=(Int24 a, Int24 b) => a._value != b._value;

        public static Int24 operator +(Int24 a, Int24 b) => a._value + b._value;

        public static Int24 operator -(Int24 a, Int24 b) => a._value - b._value;
    }
}