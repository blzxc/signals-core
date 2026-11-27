using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class OfflineMedianFilter
    {
        static public float[] Filter(float[] data, int windowSize)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (windowSize <= 0 || windowSize % 2 == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(windowSize), "Размер окна должен быть положительным и нечётным");
            }
            if (data.Length == 0)
            {
                return new float[0];
            }
            int M = (windowSize - 1) / 2;

            IEnumerable<float> x = Enumerable.Range(0, M).Select(el => data[0]);
            IEnumerable<float> z = Enumerable.Range(0, M).Select(el => data.Last());
            float[] y = x.Concat(data).Concat(z).ToArray();

            float[] newSignal = new float[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                float[] window = new float[windowSize];
                for (int j = 0; j < windowSize; j++)
                {
                    window[j] = y[i + j];
                }
                Array.Sort(window);
                newSignal[i] = window[M];
            }

            return newSignal;
        }
    }
}
