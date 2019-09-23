using System;

namespace MangoUtilities.Collections
{
    public class CircularBuffer<T>
    {
        private T[] _buffer;
        private int _length;

        public CircularBuffer(int capacity)
        {
            _buffer = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                if (index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return _buffer[Mod(index, Capacity)];
            }
            set
            {
                _buffer[Mod(index, Capacity)] = value;
                _length = Math.Max(_length, index + 1);
            }
        }

        public int Capacity => _buffer.Length;

        public int Length => _length;

        public T Last => this[Length - 1];
        
        private int Mod(int x, int m)
        {
            var r = x % m;
            
            return r < 0 ? r + m : r;
        }
    }
}