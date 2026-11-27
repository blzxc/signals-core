using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class SamplingParameters
    {
        public float SamplingRate { get; }     // частота квантования, Гц
        public float Duration { get; }         // длительность, с

        public SamplingParameters(float samplingRate, float duration)
        {
            if (samplingRate <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(samplingRate), "Частота квантования должна быть положительной");
            }
            if (duration <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(duration), "Длительность должна быть положительной");
            }
            SamplingRate = samplingRate;
            Duration = duration;
            SampleCount = (int)(samplingRate * duration + 0.5);
        }

        public int SampleCount { get; }        // число отсчётов

        public float TimeAt(int index)         // момент времени отсчёта, с
        {
            return index / SamplingRate;
        }
    }
}
