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
        private List<float> _buffer = new List<float>();

        public OnlineFilter(int windowSize)
        {
            if (windowSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(windowSize), "Размер окна должен быть положительным");
            }
            _windowSize = windowSize;
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
                if (_buffer.Count == 0)
                {
                    for (int j = 0; j < _windowSize - 1; j++)
                    {
                        _buffer.Add(data[i]);
                    }
                }

                // Добавляем новый отсчёт и удаляем вышедший из окна
                _buffer.Add(data[i]);
                if (_buffer.Count > _windowSize)
                {
                    _buffer.RemoveAt(0);
                }

                // Усредняем значения в окне
                newSignal[i] = _buffer.Sum() / _windowSize;
            }

            return newSignal;
        }
    }
}
