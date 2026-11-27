using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class Resampler
    {
        static public SingleChannelSignal Resample(SingleChannelSignal signal, float newFrequency)
        {
            if (signal == null)
            {
                throw new ArgumentNullException(nameof(signal));
            }
            if (newFrequency <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newFrequency), "Частота должна быть положительной");
            }

            float[] x = signal.Data;
            int n = x.Length;
            if (n == 0)
            {
                return new SingleChannelSignal(new float[0], newFrequency);
            }

            float freq = signal.Frequency;
            int new_n = (int)Math.Floor((n - 1) * (double)newFrequency / freq + 1e-6) + 1;
            float[] y = new float[new_n];
            for (int j = 0; j < new_n; j++)
            {
                double p = j * (double)freq / newFrequency;
                int k = (int)Math.Floor(p);
                double alpha = p - k;
                if (k >= n - 1)
                {
                    y[j] = x[n - 1];
                }
                else
                {
                    y[j] = (float)(x[k] + alpha * (x[k + 1] - x[k]));
                }
            }

            return new SingleChannelSignal(y, newFrequency);
        }
    }
}
