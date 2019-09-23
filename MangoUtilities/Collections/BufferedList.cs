using System;
using System.Collections;
using System.Collections.Generic;

namespace MangoUtilities.Collections
{
    /// <summary>
    /// A list which never removes an index, and rather replaces it with "null"
    /// </summary>
    public class BufferedList<T> : IEnumerable<T> where T : class
    {
        private T[] _buffer;
        private int _length;

        public BufferedList(int initialCapacity = 8)
        {
            _buffer = new T[initialCapacity];
            _length = 0;
        }
        
        public T this[int index]
        {
            get => _buffer[index];
            set
            {
                while (Capacity <= index)
                {
                    Capacity *= 2;
                }
                
                if (_buffer[index] == null && value != null)
                {
                    _length++;
                }
                else if (_buffer[index] != null && value == null)
                {
                    _length--;
                }
                
                _buffer[index] = value;
            }
        }

        /// <summary>
        /// Total capacity of the buffer, which scales automatically with []
        /// </summary>
        public int Capacity
        {
            get => _buffer.Length;
            set => Array.Resize(ref _buffer, value);
        }

        /// <summary>
        /// Elements in this collection which aren't null 
        /// </summary>
        public int Length => _length;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            var left = _length;
            foreach (var value in _buffer)
            {
                if (value != null)
                {
                    yield return value;

                    if (--left <= 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}