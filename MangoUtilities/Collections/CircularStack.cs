using System;
using System.Collections.Generic;

namespace MangoUtilities.Collections
{
    /// <summary>
    /// A stack which overrides old items with newer ones
    /// </summary>
    public class CircularStack<T>
    {
        private T[] _data;
        private int _nextIndex;
        private bool _circled;

        public CircularStack(int capacity)
        {
            _data = new T[capacity];
            _circled = false;
            _nextIndex = 0;
        }

        public CircularStack(int capacity, T @default) : this(capacity)
        {
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = @default;
            }
        }

        public T Push(T item)
        {
            _data[_nextIndex] = item;

            _nextIndex = GetIndex(1);

            if (_nextIndex == 0)
            {
                _circled = true;
            }
            
            return item;
        }

        public T PushSorted(T item, IComparer<T> comparator, bool allowDuplicates = true)
        {
            for (var i = 1; i < Capacity; i++)
            {
                var index = GetIndex(-i);
                var compare = comparator.Compare(item, _data[index]);
                
                if (compare > 0)
                {
                    _data[index] = item;
                    break;
                }
                
                if (compare == 0)
                {
                    if (allowDuplicates)
                    {
                        _data[index] = item;
                    }
                    break;
                }
            }

            _nextIndex = GetIndex(1);

            if (_nextIndex == 0)
            {
                _circled = true;
            }
            
            return item;
        }

        public T Peek(int n = 0) =>_data[GetIndex(-n - 1)];

        public T Pop()
        {
            _nextIndex = GetIndex(-1);

            var value = _data[_nextIndex];
            _data[_nextIndex] = default(T);
            
            return value;
        }

        public int Capacity => _data.Length;

        public int Length => _circled ? Capacity : _nextIndex;

        private int GetIndex(int n) => Mod(_nextIndex + n, Capacity);
        
        private int Mod(int x, int m)
        {
            var r = x % m;
            
            return r < 0 ? r + m : r;
        }

        public string ToString(Func<T, string> stringFunc)
        {
            var msg = "";
            for (var i = 0; i < Length; i++)
            {
                msg += stringFunc(Peek(i)) + ", ";
            }
            
            return msg;
        }

        public override string ToString()
        {
            return ToString(a => a.ToString());
        }
    }
}