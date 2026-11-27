using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class NoiseGenerator : ISignalGenerator
    {
        private float _range;
        private Func<Random> _randomFactory;

        public NoiseGenerator(float range, Func<Random> randomFactory = null)
        {
            if (range < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(range), "Размах не может быть отрицательным");
            }
            _range = range;
            if (randomFactory == null)
            {
                _randomFactory = () => new Random();
            }
            else
            {
                _randomFactory = randomFactory;
            }
        }
        public SingleChannelSignal Generate(SamplingParameters sampling)
        {
            Random random = _randomFactory();
            float[] data = new float[sampling.SampleCount];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (float)(random.NextDouble() * 2 * _range - _range);
            }
            return new SingleChannelSignal(data, sampling.SamplingRate);
        }
    }
}
