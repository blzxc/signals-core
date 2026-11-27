using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class OnlineFilter
    {
        private int _windowSize;
        private CircularBuffer<float> _buffer;
        private double _sum;
        private bool _started;

        public OnlineFilter(int windowSize)
        {
            if (windowSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(windowSize), "Размер окна должен быть положительным");
            }
            _windowSize = windowSize;
            _buffer = new CircularBuffer<float>(windowSize);
        }
        public float[] Filter(float[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            float[] newSignal = new float[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                if (!_started)
                {
                    for (int j = 0; j < _windowSize - 1; j++)
                    {
                        _buffer.Add(data[i]);
                    }
                    _sum = (double)(_windowSize - 1) * data[i];
                    _started = true;
                }

                // Добавляем новый отсчёт и удаляем вышедший из окна
                if (_buffer.IsFull)
                {
                    _sum -= _buffer[0];
                }
                _buffer.Add(data[i]);
                _sum += data[i];

                // Усредняем значения в окне
                newSignal[i] = (float)(_sum / _windowSize);
            }

            return newSignal;
        }
        public void Reset()
        {
            _buffer.Clear();
            _sum = 0;
            _started = false;
        }
    }
}
