using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class LineGenerator : ISignalGenerator
    {
        private float _k;
        private float _b;

        public LineGenerator(float k, float b)
        {
            _k = k;
            _b = b;
        }
        public SingleChannelSignal Generate(SamplingParameters sampling)
        {
            float[] data = new float[sampling.SampleCount];
            for (int i = 0; i < data.Length; i++)
            {
                float t = sampling.TimeAt(i);
                data[i] = _k * t + _b;
            }
            return new SingleChannelSignal(data, sampling.SamplingRate);
        }
    }
}
