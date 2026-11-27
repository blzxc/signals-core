using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class MeanderGenerator : ISignalGenerator
    {
        private float _period;
        private float _phase;
        private float _amplitude;
        private float _dutyCycle;

        public MeanderGenerator(float period, float phase, float amplitude, float dutyCycle)
        {
            if (!(period > 0))
            {
                throw new ArgumentOutOfRangeException(nameof(period), "Период должен быть положительным");
            }
            if (!(dutyCycle > 0 && dutyCycle < 1))
            {
                throw new ArgumentOutOfRangeException(nameof(dutyCycle), "Скважность должна быть больше 0 и меньше 1");
            }
            _period = period;
            _phase = phase;
            _amplitude = amplitude;
            _dutyCycle = dutyCycle;
        }
        public SingleChannelSignal Generate(SamplingParameters sampling)
        {
            float[] data = new float[sampling.SampleCount];
            for (int i = 0; i < data.Length; i++)
            {
                float t = sampling.TimeAt(i);
                double position = (t - _phase) / _period;
                double frac = position - Math.Floor(position);
                if (frac < _dutyCycle)
                {
                    data[i] = _amplitude;
                }
                else
                {
                    data[i] = -_amplitude;
                }
            }
            return new SingleChannelSignal(data, sampling.SamplingRate);
        }
    }
}
