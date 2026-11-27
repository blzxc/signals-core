using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class SignalOperations
    {
        static public SingleChannelSignal Sum(IReadOnlyList<SingleChannelSignal> signals)
        {
            float freq = CheckSignals(signals);

            int max_len = signals.Select(el => el.Data.Length).Max();
            float[] summ_signals = new float[max_len];
            for (int i = 0; i < signals.Count; i++)
            {
                for (int j = 0; j < signals[i].Data.Length; j++)
                {
                    summ_signals[j] += signals[i].Data[j];
                }
            }

            return new SingleChannelSignal(summ_signals, freq);
        }

        static public SingleChannelSignal Concat(IReadOnlyList<SingleChannelSignal> signals)
        {
            float freq = CheckSignals(signals);

            IEnumerable<float> concat_signals = new float[0];
            for (int i = 0; i < signals.Count; i++)
            {
                concat_signals = concat_signals.Concat(signals[i].Data);
            }

            return new SingleChannelSignal(concat_signals.ToArray(), freq);
        }

        static private float CheckSignals(IReadOnlyList<SingleChannelSignal> signals)
        {
            if (signals == null)
            {
                throw new ArgumentNullException(nameof(signals));
            }
            if (signals.Count == 0)
            {
                throw new ArgumentException("Список сигналов пуст", nameof(signals));
            }
            for (int i = 0; i < signals.Count; i++)
            {
                if (signals[i] == null)
                {
                    throw new ArgumentNullException(nameof(signals), "Сигнал с номером " + i + " равен null");
                }
            }

            float freq = signals[0].Frequency;
            double eps = 1e-5;
            for (int i = 0; i < signals.Count; i++)
            {
                if (Math.Abs(signals[i].Frequency - freq) > eps)
                {
                    throw new ArgumentException("Частоты сигналов различаются: " + freq + " Гц и " + signals[i].Frequency + " Гц", nameof(signals));
                }
            }
            return freq;
        }
    }
}
