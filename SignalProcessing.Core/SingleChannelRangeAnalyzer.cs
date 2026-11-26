using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class SingleChannelRangeAnalyzer
    {
        private float _min;
        private float _max;
        private int _minLength;

        public event EventHandler<OutOfRangeEventArgs> OutOfRange;

        public SingleChannelRangeAnalyzer(float min, float max, int minLength)
        {
            _min = min;
            _max = max;
            _minLength = minLength;
        }
        public void Analyze(float[] data)
        {
            int start = -1;

            for (int i = 0; i < data.Length; i++)
            {
                bool outOfRange = data[i] < _min || data[i] > _max;

                // Начало фрагмента с выходом за диапазон
                if (outOfRange && start == -1)
                {
                    start = i;
                }
                // Конец фрагмента, проверяем его длительность
                else if (!outOfRange && start != -1)
                {
                    if (i - start >= _minLength)
                    {
                        if (OutOfRange != null)
                        {
                            OutOfRange(this, new OutOfRangeEventArgs(start, i - 1));
                        }
                    }
                    start = -1;
                }
            }

            // Фрагмент, не закончившийся до конца сигнала
            if (start != -1 && data.Length - start >= _minLength)
            {
                if (OutOfRange != null)
                {
                    OutOfRange(this, new OutOfRangeEventArgs(start, data.Length - 1));
                }
            }
        }
    }
}
