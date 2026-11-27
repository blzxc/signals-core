using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class TriangleGenerator : ISignalGenerator
    {
        private float _period;
        private float _phase;
        private float _amplitude;

        public TriangleGenerator(float period, float phase, float amplitude)
        {
            if (!(period > 0))
            {
                throw new ArgumentOutOfRangeException(nameof(period), "Период должен быть положительным");
            }
            _period = period;
            _phase = phase;
            _amplitude = amplitude;
        }
        public SingleChannelSignal Generate(SamplingParameters sampling)
        {
            float[] data = new float[sampling.SampleCount];
            for (int i = 0; i < data.Length; i++)
            {
                float t = sampling.TimeAt(i);
                double position = (t - _phase) / _period;
                double frac = position - Math.Floor(position);
                data[i] = (float)(_amplitude * (1 - 4 * Math.Abs(frac - 0.5)));
            }
            return new SingleChannelSignal(data, sampling.SamplingRate);
        }
    }
}
