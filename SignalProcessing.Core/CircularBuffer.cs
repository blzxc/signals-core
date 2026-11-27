using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class CircularBuffer<T>
    {
        private T[] _items;
        private int _start;
        private int _count;

        public CircularBuffer(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Ёмкость должна быть положительной");
            }
            _items = new T[capacity];
        }
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
        }
        public int Count
        {
            get
            {
                return _count;
            }
        }
        public bool IsFull
        {
            get
            {
                return _count == Capacity;
            }
        }
        public void Add(T item)
        {
            if (IsFull)
            {
                _items[_start] = item;
                _start = (_start + 1) % Capacity;
            }
            else
            {
                _items[(_start + _count) % Capacity] = item;
                _count++;
            }
        }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона элементов буфера");
                }
                return _items[(_start + index) % Capacity];
            }
        }
        public T[] ToArray()
        {
            T[] result = new T[_count];
            for (int i = 0; i < _count; i++)
            {
                result[i] = _items[(_start + i) % Capacity];
            }
            return result;
        }
        public void Clear()
        {
            _start = 0;
            _count = 0;
        }
    }
}
