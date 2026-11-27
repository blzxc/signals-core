using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class OnlineMedianFilter
    {
        private int _windowSize;
        private CircularBuffer<float> _buffer;
        private bool _started;

        public OnlineMedianFilter(int windowSize)
        {
            if (windowSize <= 0 || windowSize % 2 == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(windowSize), "Размер окна должен быть положительным и нечётным");
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
                    _started = true;
                }

                _buffer.Add(data[i]);
                float[] window = _buffer.ToArray();
                Array.Sort(window);
                newSignal[i] = window[(_windowSize - 1) / 2];
            }

            return newSignal;
        }
        public void Reset()
        {
            _buffer.Clear();
            _started = false;
        }
    }
}
