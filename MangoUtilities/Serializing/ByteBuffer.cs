using System;

namespace MangoUtilities.Serializing
{
    public partial class ByteBuffer
    {
        private byte[] _data;
        private int _index, _capacity, _offset;

        /// <summary>
        /// Create a new buffer on top of provided data with custom start index and length
        /// </summary>
        public ByteBuffer(byte[] data, int capacity, int offset = 0)
        {
            _data = data;
            _offset = offset;
            _capacity = capacity;
            _index = 0;

            if (_capacity + _offset > _data.Length)
            {
                throw new Exception("Length parameter exceeds data's length");
            }
        }

        /// <summary>
        /// Create a new buffer on top of provided data
        /// </summary>
        public ByteBuffer(byte[] data) : this(data, data.Length) { }

        /// <summary>
        /// Create a new empty buffer with provided length and offset
        /// </summary>
        public ByteBuffer(int length, int offset = 0) : this(new byte[length + offset], length, offset) { }
        
        /// <summary>
        /// The raw data of the buffer
        /// </summary>
        public byte[] Data => _data;

        /// <summary>
        /// The capacity of the buffer
        /// </summary>
        public int Capacity
        {
            get => _capacity;
            set
            {
                _capacity = value;
                
                if (_data.Length < _capacity)
                {
                    Array.Resize(ref _data, _capacity);
                }
            }
        }

        public int Length => Index;

        public int Index
        {
            get => _index;
            set => _index = value;
        }
        
        public int Offset
        {
            get => _offset;
            set => _offset = value;
        }
        
        /// <summary>
        /// Reset the buffer as an O(1) operation without actual re-allocation
        /// </summary>
        public void Reset()
        {
            _offset = _index = 0;
        }

        /// <summary>
        /// Write a single byte to the buffer
        /// </summary>
        public bool Write(byte value)
        {
            if (_offset + _index >= _capacity) { return false; }
            
            _data[_offset + _index++] = value;
            return true;
        }

        /// <summary>
        /// Write multiple bytes at once to the buffer
        /// </summary>
        public bool Write(byte[] value)
        {
            if (_offset + _index + value.Length > _capacity) { return false; }

            foreach (var b in value)
            {
                _data[_offset + _index] = b;
                _index++;
            }

            return true;
        }

        /// <summary>
        /// Read a single byte from the buffer
        /// </summary>
        public byte ReadByte(bool peek = false)
        {
            byte value =  _data[_offset + _index];

            if (!peek)
            {
                _index++;
            }

            return value;
        }
    }
}
